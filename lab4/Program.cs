using System.Globalization;

public class Reader<T> {
    public List<T> readList(string path, Func<string[], T> generate) {
        List<T> list = new List<T>();
        StreamReader sr = new StreamReader(path);
        string line = sr.ReadLine();
        while( (line = sr.ReadLine()) != null) {
            list.Add(generate(line.Split(",")));
        }
        return list;
    }
}

public class Program {
    public static void Main(string[] args) {

        // 1. READ DATA
        Reader<Region> regionReader = new Reader<Region>();
        Regions regionsList = new Regions(regionReader.readList("data\\regions.csv",
                                x => new Region(x[0], x[1])));
        // Console.WriteLine(regionsList);

        Reader<Territory> territoryReader = new Reader<Territory>();
        Territories territoriesList = new Territories(territoryReader.readList("data\\territories.csv",
                                x => new Territory(x[0], x[1], x[2])));
        // Console.WriteLine(territoriesList);

        Reader<EmployeeTerritory> empTerReader = new Reader<EmployeeTerritory>();
        EmployeeTerritories empTersList = new EmployeeTerritories(empTerReader.readList("data\\employee_territories.csv",
                                x => new EmployeeTerritory(x[0], x[1])));
        // Console.WriteLine(empTersList);

        Reader<Employee> empReader = new Reader<Employee>();
        Employees employeesList = new Employees(empReader.readList("data\\employees.csv",
                                x => new Employee(x[0..18])));
        // Console.WriteLine(empsList);

        // 2. DISPLAY LASTNAMES OF EMPLOYEES
        // foreach(Employee e in empsList.data) {
        //     Console.WriteLine(e.lastName);
        // }

        // 3. DISPLAY REGION AND TERRITORY FOR EVERY EMPLOYEE
        var results3 = employeesList.data
                            .Join(empTersList.data, e => e.employeeId, et => et.employeeId, (e, et) => new {lastname = e.lastName, territoryId = et.territoryId})
                            .Join(territoriesList.data, e => e.territoryId, t => t.id, (e, t) => new {lastname = e.lastname, territory = t.description, regionId = t.regionId})
                            .Join(regionsList.data, e => e.regionId, r => r.id, (e, r) => new {lastname = e.lastname, territory = e.territory, region = r.description});

        var results31 = from e in employeesList.data
                        join et in empTersList.data on e.employeeId equals et.employeeId
                        join t in territoriesList.data on et.territoryId equals t.id
                        join r in regionsList.data on t.regionId equals r.id
                        select new { lastname = e.lastName, region = r.description, territory = t.description };




        // foreach(var r in results31.ToList()) {
        //     Console.WriteLine(r.lastname + " " + r.region + " " + r.territory);
        // }

        // 4. DISPLAY EMPLOYEES FOR EVERY REGION               
        var results4 = regionsList.data.GroupJoin(empTersList.data.Join(employeesList.data,
                                                et => et.employeeId,
                                                e => e.employeeId, 
                                                (et, e) => new {employee = e.lastName, terId = et.territoryId})
                                                .Join(territoriesList.data,
                                                et => et.terId,
                                                t => t.id,
                                                (et, t) => new {employee = et.employee, region = t.regionId}), 
                                            r => r.id,
                                            et => et.region,
                                            (r, et) => new {region = r.description, employee = et.Select(e => e.employee).Distinct()}
                                            );

        var results41 = from et in empTersList.data
                        join e in employeesList.data on et.employeeId equals e.employeeId
                        join t in territoriesList.data on et.territoryId equals t.id
                        join r in regionsList.data on t.regionId equals r.id
                        group e by r.id into grouped
                        select new {
                            employee = grouped.Select(e => e.lastName).Distinct().ToList(), 
                            region = grouped.Key
                            };

        // foreach(var r in results4) {
        //     Console.Write("{0}:", r.region);
        //     foreach(var e in r.employee) {
        //         Console.Write(" {0}", e);
        //     }
        //     Console.WriteLine();
        // }
        // foreach(var r in results41) {
        //     Console.Write("{0}:", r.region);
        //     foreach(var e in r.employee) {
        //             Console.Write(" {0}", e);
        //     }
        //     Console.WriteLine();
        // }

        // 5. NUMBER OF EMPLOYEES IN EVERY REGION
        var results5 = regionsList.data.GroupJoin(empTersList.data.Join(employeesList.data,
                                                et => et.employeeId,
                                                e => e.employeeId, 
                                                (et, e) => new {employee = e.lastName, terId = et.territoryId})
                                                .Join(territoriesList.data,
                                                et => et.terId,
                                                t => t.id,
                                                (et, t) => new {employee = et.employee, region = t.regionId}), 
                                            r => r.id,
                                            et => et.region,
                                            (r, et) => new {region = r.description, number = et.Select(e => e).Distinct().Count()}
                                            );

        var results51 = from et in empTersList.data
                        join e in employeesList.data on et.employeeId equals e.employeeId
                        join t in territoriesList.data on et.territoryId equals t.id
                        join r in regionsList.data on t.regionId equals r.id
                        group e by r.description into grouped
                        select new {
                            region = grouped.Key,
                            number = grouped.Distinct().Count()
                        };

        // foreach(var r in results51) {
        //     Console.WriteLine("{0}: {1}", r.region, r.number);
        // }

        // 6. NUMBER OF ORDERS, AVERAGE VALUE AND MAX ORDER FOR EVERY EMPLOYEE
        Reader<Order> ordersReader = new Reader<Order>();
        Orders ordersList = new Orders(ordersReader.readList("data\\orders.csv",
                                x => new Order(x[0..14])));

        Reader<OrderDetails> odReader = new Reader<OrderDetails>();
        OrdersDetails ordList = new OrdersDetails(odReader.readList("data\\orders_details.csv",
                                x => new OrderDetails(x[0], x[1], x[2], x[3], x[4])));

        var results6 = employeesList.data.GroupJoin(ordersList.data.Join(ordList.data,
                                                                        or => or.orderId,
                                                                        de => de.orderId,
                                                                        (or, de) => new {employee = or.employeeId,
                                                                                        value = float.Parse(de.unitprice ?? "0", CultureInfo.InvariantCulture) * float.Parse(de.quantity ?? "0", CultureInfo.InvariantCulture)}),
                                                    e => e.employeeId,
                                                    o => o.employee,
                                                    (e, o) => new {employee = e.employeeId,
                                                                    number = o.Select(o => o).Count(),
                                                                    average = o.Select(o => o.value).Average(),
                                                                    max = o.Select(o => o.value).Max()
                                                                    }
                                                    );
        
        var results61 = from x in (
                            from o in ordersList.data
                            join de in ordList.data on o.orderId equals de.orderId
                            join e in employeesList.data on o.employeeId equals e.employeeId
                            select new {
                                employee = e,
                                value = float.Parse(de.unitprice ?? "0", CultureInfo.InvariantCulture) * float.Parse(de.quantity ?? "0", CultureInfo.InvariantCulture)
                            }
                        )
                        group x by x.employee.employeeId into grouped
                        select new {
                            employee = grouped.Key,
                            number = grouped.Count(),
                            average = grouped.Average(gr => gr.value),
                            max = grouped.Max(gr => gr.value)
                        };


        foreach(var r in results61) {
            Console.WriteLine("{0}:\namount: {1}\naverage: {2}\nmax: {3}\n", r.employee, r.number, r.average, r.max);
        }
    }
}