        
        
        
    public (Truck Truck, Crate Crate) Contains()
    {
        Truck t = Array.Find(Map.Trucks,
            truck => (truck.x == x && truck.y == y)
        );

        Crate c = Array.Find(Map.Crates,
            crate => (crate.x == x && crate.y == y)
        );

        return (Truck: t, Crate: c);
    }



