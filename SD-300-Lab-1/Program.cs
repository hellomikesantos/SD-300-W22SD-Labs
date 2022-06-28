// Lab-4
Hotel briskHotel = new Hotel("Brisk Summer Lodge", "123 Amazing Place Street");

briskHotel.CreateNewRoom("101", 4, "Regular");
briskHotel.CreateNewRoom("102", 4, "Regular");
briskHotel.CreateNewRoom("103", 2, "Premium");

Client client1 = new Client("Michael", 12345, "Regular");
Client client2 = new Client("Oliver", 23433, "Premium");
Client client3 = new Client("Rev", 23433, "Regular");

briskHotel.MakeReservation(client2, "103", 2, new DateTime(2022, 9, 29, 13, 0, 0));
briskHotel.MakeReservation(client1, "102", 3, new DateTime(2022, 7, 29, 13, 0, 0));
briskHotel.MakeReservation(client3, "101", 1, new DateTime(2022, 7, 29, 13, 0, 0));

briskHotel.CheckAllGuests();
briskHotel.CheckAllRooms();
class Hotel
{
    public static string Name { get; set; }
    public static string Address { get; set; }
    public Room Room { get; set; }
    public Reservation Reservation { get; set; }
    public List<Room> Rooms { get; set; } = new List<Room>();
    public List<Client> Clients { get; set; } = new List<Client>();
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    public Hotel(string name, string address)
    {
        Name = name;
        Address = address;
        Room = new Room("100", 4, "Economy", this);
        Reservation = Room.Reservation;
        Room.Hotel = this;
    }
    public void CreateNewRoom(string roomNumber, int capacity, string rating) // instantiates a new room and sets its properties
    {
        Room room = new Room(roomNumber, capacity, rating, this);
        Room = room;
        Reservation = Room.Reservation;
        Room.Hotel = this;
    }
    public void CheckAllRooms() // displays all rooms in this hotel and their details: Room number, max occupants, rating
    {
        Console.WriteLine("=========================== Check All Rooms ====================================");
        Console.WriteLine($"All {Rooms.Count} rooms have the following details:");
        foreach(Room room in Rooms)
        {
            Console.WriteLine($"Room: {room.Number} has {room.Occupants} guests/occupants ----- Rating: {room.Rating}, Number: {room.Number}");
            //Console.WriteLine($"Rating: {room.Rating}, Number: {room.Number}");
        }
    }
    public void CheckAllGuests() // displays all guests in this hotel and their details: their checked room, name, membership and how many they are checked-in
    {
        Console.WriteLine($"There are a total of {Clients.Count} clients in {Name}");
        Console.WriteLine(Clients.Count);
        foreach (Client client in Clients)
        {
            try
            {
                Console.WriteLine("++++++++++++++++++++++++++++++ Check All Guests +++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine($"Guest Name: {client.Name}");
                Console.WriteLine($"Guest Membership: {client.Membership}");
                Console.WriteLine($"Room No.: {client.Reservation.Room.Number}");
                Console.WriteLine($"No. of Occupants: {client.Reservation.Occupants}");
            }
            catch (Exception ex)
            {
            Console.WriteLine(ex.Message);
            }
        }
    }
    /**
     // MakeReservation() performs the creation of a reservation, in all rooms in this hotel, it creates an instance of a Reservation - a successful reservation is:
    - correct room number, if a room is not yet occupied and if the room's rating is similar to guest's membership e.g. "Regular", "Premium"
    **/
    public void MakeReservation(Client client, string roomNumber, int occupants, DateTime date) 
    {
        // handle reservation
        foreach (Room r in Rooms) // find the room and check if available and/or capacity available
        {
            if (r.Number == roomNumber && !r.Occupied && r.Rating == client.Membership)
            {
                Reservation newReserve = new Reservation(r, this);
                if(newReserve.Room.Capacity >= occupants)
                {
                    newReserve.Occupants = occupants;
                    newReserve.Date = date;
                    newReserve.IsCurrent = true;
                    newReserve.Client = client;
                    client.Reservation = newReserve;
                    client.Hotel = this;
                    Clients.Add(client);
                    Reservations.Add(newReserve);
                    newReserve.Room.Occupants = occupants;
                    Console.WriteLine("***************************************************");
                    Console.WriteLine($"Reservation created successfully:");
                    Console.WriteLine($"Name: {newReserve.Client.Name}");
                    Console.WriteLine($"Room: {newReserve.Room.Number}");
                    Console.WriteLine($"No. of Occupants: {newReserve.Occupants}");
                    Console.WriteLine($"Reservation Date: {newReserve.Date}");
                    Console.WriteLine("################### END ################");
                    break;
                }
            }
        }
    }
}

class Client
{
    public Hotel Hotel { get; set; }
    public Room Room { get; set; }
    public string Name { get; set; }
    public long CreditCard { get; set; }
    public string Membership { get; set; }
    public List<Reservation> Reservations { get; set; }
    public Reservation Reservation { get; set; }
    public Client(string name, long creditCard, string membership)
    {
        Name = name;
        CreditCard = creditCard;
        Membership = membership;
    }
}

class Reservation
{
    public DateTime Date { get; set; } 
    public int Occupants { get; set; }
    public bool IsCurrent { get; set; } = false;
    public Hotel Hotel { get; set; }
    public Client Client { get; set; }
    public List<Client> Clients { get; set; }
    public Room Room { get; set; }
    
    public Reservation(Room room, Hotel hotel)
    {
        Room = room;
        Hotel = hotel;
    }
}

class Room
{
    public Hotel Hotel { get; set; } 
    public string Number { get; set; }
    public int Capacity { get; set; }
    public bool Occupied { get; set; } = false;
    public string Rating { get; set; }
    public int Occupants { get; set; }
    public List<Reservation> Reservations { get; set; }
    public Reservation Reservation { get; set; }
    public Room(string number, int capacity, string rating, Hotel hotel)
    {
        Number = number;
        Capacity = capacity;
        Rating = rating;
        Hotel = hotel;
        Hotel.Rooms.Add(this);
        Reservation = new Reservation(this, hotel);
        Reservation.Clients = Hotel.Clients;
        Reservation.Room = this;
        Reservation.Hotel = hotel;
    }
}