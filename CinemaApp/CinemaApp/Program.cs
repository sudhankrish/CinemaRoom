using System;

namespace CinemaApp
{
    #region Requirements
    /**
     * The owners of a Cinema theater have contracted you to develop the backend of a new app, targeted both customers and internal staff. 
       The app should include several features for a given cinema room.
       When developing the application, the clients is especially interested in ensuring the following:
        • The application is expected to be developed further over time. Make sure this can be done without introducing new errors to the existing features.
        • Not all users are expected to be IT experts. Therefore, make sure that invalid inputs are handled properly and users are guided well.
        • Not all seats are equal! If the capacity is over 50 people, the front half rows cost $12, the back half cost $10. If capacity 50 or below, all cost $10

     * Build a console application in Java or C#, that implements the following features:
            1. Receive inputs for "number of rows" and "number of seats per row" for the given cinema room.
            2. Show an output of the seats and their status ("A" for available, "R" for reserved)
            3. Buy a ticket for a specific seat. The user should be able to choose which seat, however only available ones.
            4. Output metrics for the cinema room, including:
                A. Number of purchased tickets
                B. Percentage occupied
                C. Current income (sum of reserved tickets)
                D. Potential total income (sum of all available and reserved tickets)
            When the application starts up, the user should be able to choose which action to take. 
            Only upon actively choosing to stop the app, should the application end the runtime.
     * */
    #endregion 

    class Program
    {
        static char[,] currentRoom;
        const string cinemaRoomHeader = " ROOM DETAILS ";
        const string availabilityHeader = "CURRENT STATUS";
        const string buyTicketHeader = "  BUY TICKET  ";
        const string showMetricsHeader = " SHOW METRICS ";
        static void Main(string[] args)
        {
            initializeCurrentRoom();
            int menuOption;
            do
            {
                printMenu();
                while (!int.TryParse(Console.ReadLine(), out menuOption))
                {
                    Console.WriteLine("Wrong input. Please choose valid options.");
                }

                switch (menuOption)
                {
                    case 1:
                        printHeader(availabilityHeader);
                        showAvailability();
                        break;
                    case 2:
                        printHeader(buyTicketHeader);
                        buyTicket();
                        break;
                    case 3:
                        printHeader(showMetricsHeader);
                        outputMetrics();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Wrong input!");
                        Console.ReadLine();
                        break;
                }
            } while (menuOption != 0);
        }


        /// <summary>
        /// This method will print header text of the selected option
        /// </summary>
        /// <param name="text">display text</param>
        private static void printHeader(string text)
        {
            string printEmptyLine = "*                                            *";
            string insertHeader = printEmptyLine.Remove(14, text.Length);
            insertHeader = insertHeader.Insert(14, text);
            Console.WriteLine("**********************************************");
            Console.WriteLine(printEmptyLine);
            Console.WriteLine(insertHeader);
            Console.WriteLine(printEmptyLine);
            Console.WriteLine("**********************************************");
        }

        /// <summary>
        /// Method to show status availability in current room
        /// </summary>
        private static void showAvailability()
        {
            Console.WriteLine();
            Console.WriteLine("Current Availability of seats:");
            Console.WriteLine(" ");

            for (int i = 0; i < currentRoom.GetLength(0); i++)
            {
                for (int j = 0; j < currentRoom.GetLength(1); j++)
                {
                    Console.Write(" '{0}'", currentRoom[i, j]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Calculate the price per ticket
        /// </summary>
        /// <param name="row">input row</param>
        /// <returns>the price</returns>
        private static int getPricePerTicket(int row)
        {
            int seats = currentRoom.Length;
            int pricePerTicket = 10;

            if (seats > 50 && row > currentRoom.Length / 2)
            {
                pricePerTicket = 12;
            }
            return pricePerTicket;
        }

        /// <summary>
        /// Initialize the current room
        /// </summary>
        private static void initializeCurrentRoom()
        {
            bool validInput = false;
            printHeader(cinemaRoomHeader);
            do
            {
                Console.WriteLine("Enter the number of rows of current Room:");
                int rows;
                while (!int.TryParse(Console.ReadLine(), out rows))
                {
                    Console.Write("This is not valid input. Please enter a valid integer value for number of rows: ");
                }

                Console.WriteLine("Enter the number of seats in each row of current room:");
                int seatsPerRow;
                while (!int.TryParse(Console.ReadLine(), out seatsPerRow))
                {
                    Console.WriteLine("This is not valid input. Please enter valid input for number of seats in each row: ");
                }

                if (rows <= 0 || seatsPerRow <= 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Wrong input!");
                    Console.WriteLine();
                }
                else
                {
                    validInput = true;
                    currentRoom = new char[rows, seatsPerRow];
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < seatsPerRow; j++)
                        {
                            currentRoom[i, j] = 'A';
                        }
                    }
                }
            } while (!validInput);
        }

        /// <summary>
        /// Book a seat - Update the status to 'R'
        /// </summary>
        /// <param name="row"></param>
        /// <param name="seat"></param>
        private static void bookSeat(int row, int seat)
        {
            currentRoom[row - 1, seat - 1] = 'R';
        }

        /// <summary>
        /// Menu for the user to navigate
        /// </summary>
        private static void printMenu()
        {
            Console.WriteLine();
            Console.WriteLine("To see current status of seats - Enter '1'");
            Console.WriteLine("To buy a ticket - Enter '2'");
            Console.WriteLine("To view metrics - Enter '3'");
            Console.WriteLine("If you wish to close the program - Enter '4'");
            Console.WriteLine();
            Console.WriteLine("Enter any one of the above options and press Enter.");
        }

        /// <summary>
        /// Method to buy a particular ticket
        /// </summary>
        private static void buyTicket()
        {
            bool isValidInput = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Enter a row number:");

                int row;
                while (!int.TryParse(Console.ReadLine(), out row))
                {
                    Console.WriteLine("Please enter valid input for row number: ");
                    return;
                }

                Console.WriteLine("Enter a seat number in that row:");
                int seat;
                while (!int.TryParse(Console.ReadLine(), out seat))
                {
                    Console.WriteLine("Please enter valid input for seat number ! ");
                    return;
                }

                if (row <= 0 || row > currentRoom.GetLength(0) || seat <= 0 || seat > currentRoom.GetLength(1))
                {
                    Console.WriteLine();
                    Console.WriteLine("Wrong input!");
                }
                else if (currentRoom[row - 1, seat - 1] == 'R')
                {
                    Console.WriteLine();
                    Console.WriteLine("The selected ticket has already been purchased! Please enter different combination of row/seat number.");
                }
                else
                {
                    isValidInput = true;
                    bookSeat(row, seat);
                    Console.WriteLine("Ticket Price: ${0}", getPricePerTicket(row));
                }
            } while (!isValidInput);
        }

        /// <summary>
        /// Method to display various output metrics
        /// </summary>
        private static void outputMetrics()
        {
            int ticketsSold = 0;
            int currentIncome = 0;
            int potentialTotalIncome = 0;
            int totalSeats = currentRoom.GetLength(0) * currentRoom.GetLength(1);

            for (int row = 1; row <= currentRoom.GetLength(0); row++)
            {
                int seatPrice = getPricePerTicket(row);
                for (int seat = 1; seat <= currentRoom.GetLength(1); seat++)
                {
                    if (currentRoom[row - 1, seat - 1] == 'R')
                    {
                        ticketsSold++;
                        currentIncome += seatPrice;
                    }
                    potentialTotalIncome += seatPrice;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Number of purchased tickets: {0}", ticketsSold);
            Console.WriteLine("Percentage: {0}%", (float)ticketsSold / totalSeats * 100);
            Console.WriteLine("Current income: ${0}", currentIncome);
            Console.WriteLine("Potential total income: ${0}", potentialTotalIncome);
        }

    }
}