using System;

namespace CommunityLibrarySystem
{
    public class Movie
    {
        private string titleOfMovie;
        private string genreOfMovie;
        private string classification;
        private int MovieDuration;
        private int availableNoOfCopies;
        private int countingBorrowOfMovie;  // This property tracks the borrow count

        private static readonly string[] availableGenres = { "drama", "family", "animated", "adventure", "sci-fi", "action",  "comedy",  "thriller", "other" };
        private static readonly string[] classificationsAvailable = { "G", "PG", "M15+", "MA15+" };

        public Movie(string title, string genre, string classification, int duration, int availableNoofcopies)
        {
            this.titleOfMovie = title;
            this.genreOftheMovie = genre;
            this.classificationOfMovie = classification;
            this.MovieDuration = duration;
            this.availableNoOfCopies = availableNoofcopies;
            this.countingBorrowOfMovie = 0;  // We are initializing the borrow count to 0
        }

        // For encapsulated fields, We are using Public getter and setter methods 

        public string gettingTitle() => titleOfMovie;
        public void settingTitle(string value) => titleOfMovie = value;

        //public property for genreOftheMovie
        public string genreOftheMovie
        {
            get => genreOfMovie;
            set
            {
                //checking if the genreOfMovie provided matches the predefined list of genreOfMovie
                if (Array.Exists(availableGenres, g => g.Equals(value, StringComparison.OrdinalIgnoreCase)))
                {
                    genreOfMovie = value;
                }
                else
                {
                    //Throw exception if the genreOfMovie provided by the user does not exist
                    throw new ArgumentException($"Please make sure the genreOfMovie is one of the following: {string.Join(", ", availableGenres)}");
                }
            }
        }

        //public property for classification
        public string classificationOfMovie
        {
            get => classification;
            set
            {
                //checking if the classification provided by suer is in the predefined list of classification
                if (Array.Exists(classificationsAvailable, c => c.Equals(value, StringComparison.OrdinalIgnoreCase)))
                {
                    classification = value;
                }
                else
                {
                    //Throw exception if it doesnt exist
                    throw new ArgumentException($"Please make sure the classification is one of the following: {string.Join(", ", classificationsAvailable)}");
                }
            }
        }
        //getter and setter for MovieDuration, available number of copies and borrow count in the system
        public int gettingDuration() => MovieDuration;
        public void settingDuration(int value) => MovieDuration = value;

        public int gettingCopiesAvailable() => availableNoOfCopies;
        public void settingCopiesAvailable(int value) => availableNoOfCopies = value;

        public int gettingBorrowCount() => countingBorrowOfMovie;
        public void BorrowCountIncrement() => countingBorrowOfMovie++;
        public void decrementAvailableCopies() => availableNoOfCopies--;
        public void incrementAvailableCopies() => availableNoOfCopies++;

        //String representation of the movie is provided by overriding the ToString method
        public override string ToString()
        {
            return $"{titleOfMovie}, {genreOfMovie}, {classification}, {MovieDuration} min, {availableNoOfCopies}: available no of copies , has been borrowed {countingBorrowOfMovie} times";
        }
    }

    public class MovieCollection
    {
        private const int fixedTableSize = 1000;
        private Movie[] movies;
        private bool[] occupied;

        public MovieCollection()
        {
            movies = new Movie[fixedTableSize];
            occupied = new bool[fixedTableSize];
        }
        //using double hashing technique to avoid collision
        private int HashNoOne(string key)
        {
            int hash = 0;
            key = key.ToLower(); 
            foreach (char c in key)
            {
                hash = (hash * 31 + c) % fixedTableSize;
            }
            return hash;
        }

        private int HashNoTwo(string key)
        {
            int hash = 0;
            key = key.ToLower(); 
            foreach (char c in key)
            {
                hash = (hash * 17 + c) % (fixedTableSize - 1);
            }
            return hash + 1;
        }


        public void addMovieInSystem(Movie movie)
        {
            string key = movie.gettingTitle().ToLower(); //Making sure case is insensitive
            int index = HashNoOne(key);
            int sizeOfStep = HashNoTwo(key);

            while (occupied[index])
            {
                index = (index + sizeOfStep) % fixedTableSize;
            }

            movies[index] = movie;
            occupied[index] = true;
        }

        public Movie? gettingMovie(string title)
        {
            string key = title.ToLower();
            int index = HashNoOne(key);
            int theStepSize = HashNoTwo(key);

            while (occupied[index])
            {
                if (movies[index] != null && movies[index].gettingTitle().ToLower() == key)
                {
                    return movies[index];
                }
                index = (index + theStepSize) % fixedTableSize;
            }

            return null;
        }

        public void removingMoviefromsystem(string title)
        {
            int index = HashNoOne(title.ToLower());  // Input is converted to lower case for hashing
            int sizeOfStep = HashNoTwo(title.ToLower());  // For the secondary hash, using the lowercase title Of Movie 

            while (occupied[index])
            {
                if (movies[index] != null && movies[index].gettingTitle().ToLower() == title.ToLower())  // Comparing the titles of the movies in lowercase
                {
                    movies[index] = null;
                    occupied[index] = false;
                    return;
                }
                index = (index + sizeOfStep) % fixedTableSize;
            }
        }


        public Movie[] getAllMoviesInSystem()
        {
            int count = 0;
            for (int n = 0; n < movies.Length; n++)
            {
                if (movies[n] != null)
                {
                    count++;
                }
            }

            Movie[] allMoviesInSystem = new Movie[count];
            int index = 0;
            for (int n = 0; n < movies.Length; n++)
            {
                if (movies[n] != null)
                {
                    allMoviesInSystem[index++] = movies[n];
                }
            }

            //The array sorts the movies
            Array.Sort(allMoviesInSystem, (x, y) => string.Compare(x.gettingTitle(), y.gettingTitle(), StringComparison.Ordinal));
            return allMoviesInSystem;
        }

        //get the top 3 borrowed movies
        public Movie[] gettingTopThreeBorrowedMovies()
        {
            Movie[] allTheMovies = getAllMoviesInSystem();
            Array.Sort(allTheMovies, (x, y) => y.gettingBorrowCount().CompareTo(x.gettingBorrowCount()));
            Movie[] topThreeMovies = new Movie[Math.Min(3, allTheMovies.Length)];

            for (int n = 0; n < topThreeMovies.Length; n++)
            {
                topThreeMovies[n] = allTheMovies[n];
            }

            return topThreeMovies;
        }
    }

    public class Member
    {
        private string firstName;
        private string lastName;
        private string phoneNo;
        private string password;
        private Movie[] borrowedMoviesInSystem;
        private int borrowedMoviesCount;

        public Member(string firstName, string lastName, string phoneNumber, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNo = phoneNumber;
            this.password = password;
            this.borrowedMoviesInSystem = new Movie[5];
            this.borrowedMoviesCount = 0;
        }

        // Public getter and setter methods

        public string FirstName() => firstName;
        public void setFirstName(string value) => firstName = value;

        public string LastName() => lastName;
        public void setLastName(string value) => lastName = value;

        public string getPhoneNo() => phoneNo;
        public void setPhoneNo(string value) => phoneNo = value;

        public string getPass() => password;
        public void setPass(string value) => password = value;

        public int getBorrowedMoviesCount() => borrowedMoviesCount;
        public Movie[] GetMoviesBorrowed() => borrowedMoviesInSystem;

        public bool movieBorrow(Movie movie)
        {
            if (borrowedMoviesCount < 5 && !moviesBorrowed(movie.gettingTitle()))
            {
                borrowedMoviesInSystem[borrowedMoviesCount++] = movie;
                return true;
            }
            return false;
        }

        public bool returningMovie(Movie movie)
        {
            for (int n = 0; n < borrowedMoviesCount; n++)
            {
                if (borrowedMoviesInSystem[n].gettingTitle() == movie.gettingTitle())
                {
                    for (int m = n; m < borrowedMoviesCount - 1; m++)
                    {
                        borrowedMoviesInSystem[m] = borrowedMoviesInSystem[m + 1];
                    }
                    borrowedMoviesInSystem[--borrowedMoviesCount] = null!;
                    return true;
                }
            }
            return false;
        }

        public bool moviesBorrowed(string title)
        {
            for (int n = 0; n < borrowedMoviesCount; n++)
            {
                if (borrowedMoviesInSystem[n].gettingTitle() == title)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return $"{firstName} {lastName} : {phoneNo}";
        }
    }

    public class MemberCollection
    {
        private Member[] members;
        private int count;

        public MemberCollection(int max = 1000)
        {
            members = new Member[max];
            count = 0;
        }

        public bool addingMember(Member member)
        {
            if (gettingMember(member.FirstName(), member.LastName()) != null)
            {
                Console.WriteLine("Member is already registered.");
                return false;
            }

            if (count < members.Length)
            {
                members[count++] = member;
                return true;
            }
            return false;
        }

        public bool RemoveMember(string firstName, string lastName)
        {
            string lowerFirstName = firstName.ToLower(); // Making sure first name is case-insensitive
            string lowerLastName = lastName.ToLower(); // Making sure last name is case-insensitive

            for (int n = 0; n < count; n++)
            {
                if (members[n].FirstName().ToLower() == lowerFirstName &&
                    members[n].LastName().ToLower() == lowerLastName)
                {
                    if (members[n].getBorrowedMoviesCount() == 0)
                    {
                        for (int m = n; m < count - 1; m++)
                        {
                            members[m] = members[m + 1];
                        }
                        members[--count] = null;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("As a member please return all the borrowed movies first.");
                        return false;
                    }
                }
            }
            Console.WriteLine("This member can't be found in the system.");
            return false;
        }


        public Member? gettingMember(string firstName, string lastName)
        {
            string lowerFirstName = firstName.ToLower(); // Converting the input first name to lowercase
            string lowerLastName = lastName.ToLower(); // Converting the input last name to lowercase

            for (int n = 0; n < count; n++)
            {
                if (members[n].FirstName().ToLower() == lowerFirstName &&
                    members[n].LastName().ToLower() == lowerLastName)
                {
                    return members[n];
                }
            }
            return null;
        }

        public Member[] gettingAllMembers()
        {
            Member[] currentMembers = new Member[count];
            for (int n = 0; n < count; n++)
            {
                currentMembers[n] = members[n];
            }
            return currentMembers;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MovieCollection movieCollection = new MovieCollection();
                MemberCollection memberCollection = new MemberCollection();
                bool exit = false;

                while (!exit)
                {
                    try
                    {
                        Console.WriteLine("===============================================");
                        Console.WriteLine("COMMUNITY LIBRARY MOVIE DVD MANAGEMENT SYSTEM");
                        Console.WriteLine("===============================================");
                        Console.WriteLine("Main Menu");
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("Select from the following:");
                        Console.WriteLine("1. Staff");
                        Console.WriteLine("2. Member");
                        Console.WriteLine("3. End the program");
                        Console.Write("Enter your choice ==> ");
                        string option = Console.ReadLine();

                        switch (option)
                        {
                            case "1":
                                StaffMenu(movieCollection, memberCollection);
                                break;
                            case "2":
                                MemberMenu(movieCollection, memberCollection);
                                break;
                            case "3":
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Please try again as the choice is Invalid");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"There is an error, please try again: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is an error, please try again: {ex.Message}");
            }
        }

        static void StaffMenu(MovieCollection movieCollection, MemberCollection memberCollection)
        {
            Console.Write("Please enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Please enter your password: ");
            string password = Console.ReadLine();

            if (username == "staff" && password == "today123")
            {
                bool exit = false;
                while (!exit)
                {
                    try
                    {
                        Console.WriteLine("Staff Menu");
                        Console.WriteLine("-----------------------------------------------------");
                        Console.WriteLine("1. Add DVDs to system");
                        Console.WriteLine("2. Remove DVDs from system");
                        Console.WriteLine("3. Register a new member to system");
                        Console.WriteLine("4. Remove a registered member from the system");
                        Console.WriteLine("5. Find a member contact phone number, given the member's name");
                        Console.WriteLine("6. Find Members who are currently renting a particular movie");
                        Console.WriteLine("7. Return to Main Menu");
                        Console.Write("Enter your choice ==> ");
                        string option = Console.ReadLine();

                        switch (option)
                        {
                            case "1":
                                addingMovie(movieCollection);
                                break;
                            case "2":
                                removingMovieFromSystem(movieCollection);
                                break;
                            case "3":
                                registeringMember(memberCollection);
                                break;
                            case "4":
                                removingMember(memberCollection);
                                break;
                            case "5":
                                findMemberContactNumber(memberCollection);
                                break;
                            case "6":
                                findingMembersBorrowingMovie(memberCollection);
                                break;
                            case "7":
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Please try again as it is an Invalid choice");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"There is an error, please try again: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("The credentials are invalid, the access is denied.");
            }
        }

        static void MemberMenu(MovieCollection movieCollection, MemberCollection memberCollection)
        {
            Console.Write("Please enter your first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Please enter your last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Please enter your password: ");
            string password = Console.ReadLine();

            Member? member = memberCollection.gettingMember(firstName, lastName);
            if (member != null && member.getPass() == password)
            {
                bool exit = false;
                while (!exit)
                {
                    try
                    {
                        Console.WriteLine("Member Menu");
                        Console.WriteLine("--------------------------------------------");
                        Console.WriteLine("1. Browse all the Movies");
                        Console.WriteLine("2. Display all the information about a movie, given the movieTitle of the movie");
                        Console.WriteLine("3. Borrow a movie DVD");
                        Console.WriteLine("4. Return a movie DVD");
                        Console.WriteLine("5. List current borrowing movies");
                        Console.WriteLine("6. Display the top 3 movies rented by members");
                        Console.WriteLine("7. Return to Main Menu");
                        Console.Write("Enter your choice ==> ");
                        string choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                displayMovies(movieCollection);
                                break;
                            case "2":
                                displayInfoOfMovie(movieCollection);
                                break;
                            case "3":
                                borrowTheMovie(movieCollection, member);
                                break;
                            case "4":
                                returnTheMovie(movieCollection, member);
                                break;
                            case "5":
                                listMoviesBorrowed(member);
                                break;
                            case "6":
                                displayTopThreeBorrowedMovies(movieCollection);
                                break;
                            case "7":
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("The choice is not valid, Please try again.");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"There is an error, please try again: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("The credentials are invalid, so access is denied.");
            }
        }

        static void addingMovie(MovieCollection movieCollection)
        {
            try
            {
                Console.Write("Please enter the titleOfMovie of the movie: ");
                string title = Console.ReadLine();
                Console.Write("Please enter the genreOfMovie of the movie: ");
                string genre = Console.ReadLine();
                Console.Write("Please enter the classification of the movie: ");
                string classification = Console.ReadLine();
                Console.Write("Please enter the MovieDuration (in minutes) of the movie: ");
                int duration = int.Parse(Console.ReadLine());
                Console.Write("Please enter the number of copies: ");
                int copies = int.Parse(Console.ReadLine());

                Movie movie = movieCollection.gettingMovie(title);
                if (movie == null)
                {
                    movie = new Movie(title, genre, classification, duration, copies);
                    movieCollection.addMovieInSystem(movie);
                }
                else
                {
                    movie.settingCopiesAvailable(movie.gettingCopiesAvailable() + copies);
                }
                Console.WriteLine("The movie is added sucessfully to the system");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is an error, please try again: {ex.Message}");
            }
        }

        static void removingMovieFromSystem(MovieCollection movieCollection)
        {
            try
            {
                Console.Write("Please enter the movieTitle of the movie to be removed: ");
                string title = Console.ReadLine();
                Movie movie = movieCollection.gettingMovie(title);
                if (movie != null)
                {
                    Console.Write("Please enter the number of copies to be removed from the system: ");
                    int copiesToRemove = int.Parse(Console.ReadLine());
                    if (copiesToRemove >= movie.gettingCopiesAvailable())
                    {
                        movieCollection.removingMoviefromsystem(title);
                    }
                    else
                    {
                        movie.settingCopiesAvailable(movie.gettingCopiesAvailable() - copiesToRemove);
                    }
                    Console.WriteLine("The movie is removed successfully from the system.");
                }
                else
                {
                    Console.WriteLine("This movie cannot be found in the system.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is an error, please try again: {ex.Message}");
            }
        }

        static void registeringMember(MemberCollection memberCollection)
        {
            try
            {
                Console.Write("Please enter your first name: ");
                string firstName = Console.ReadLine();
                Console.Write("Please enter your last name: ");
                string lastName = Console.ReadLine();
                Console.Write("Please enter your contact number: ");
                string phoneNumber = Console.ReadLine();
                string password;
                while (true)
                {
                    Console.Write("Please enter a four digit password: ");
                    password = Console.ReadLine();
                    if (password.Length == 4 && int.TryParse(password, out _))
                    {
                        break;
                    }
                    Console.WriteLine("The password you have written is invalid. Please enter a four digit number.");
                }

                Member member = new Member(firstName, lastName, phoneNumber, password);
                if (memberCollection.addingMember(member))
                {
                    Console.WriteLine("The member has been registered successfully in the system.");
                }
                else
                {
                    Console.WriteLine("The member cannot be registered. The member might already exist or the number of members are full");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is an error, please try again: {ex.Message}");
            }
        }

        static void removingMember(MemberCollection memberCollection)
        {
            try
            {
                Console.Write("Please enter your first name: ");
                string firstName = Console.ReadLine();
                Console.Write("Please enter your last name: ");
                string lastName = Console.ReadLine();

                if (memberCollection.RemoveMember(firstName, lastName))
                {
                    Console.WriteLine("The member has been removed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is an error, please try again: {ex.Message}");
            }
        }

        static void findMemberContactNumber(MemberCollection memberCollection)
        {
            try
            {
                Console.Write("Please enter your first name: ");
                string firstName = Console.ReadLine().ToLower(); 
                Console.Write("Please enter your last name: ");
                string lastName = Console.ReadLine().ToLower(); 

                Member? member = memberCollection.gettingMember(firstName, lastName);
                if (member != null)
                {
                    Console.WriteLine($"Contact Number: {member.getPhoneNo()}");
                }
                else
                {
                    Console.WriteLine("This member cannot be found in the system.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is an error, please try again: {ex.Message}");
            }
        }


        static void findingMembersBorrowingMovie(MemberCollection memberCollection)
        {
            try
            {
                Console.Write("Please enter the titleOfMovie of the movie: ");
                string title = Console.ReadLine();

                Member[] members = memberCollection.gettingAllMembers();
                foreach (var member in members)
                {
                    if (member != null && member.moviesBorrowed(title))
                    {
                        Console.WriteLine(member);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is an error, please try again: {ex.Message}");
            }
        }

        static void displayMovies(MovieCollection movieCollection)
        {
            try
            {
                Movie[] movies = movieCollection.getAllMoviesInSystem();
                for (int n = 0; n < movies.Length; n++)
                {
                    Console.WriteLine(movies[n]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is an error, please try again: {ex.Message}");
            }
        }

        static void displayInfoOfMovie(MovieCollection movieCollection)
        {
            try
            {
                Console.Write("Please enter the titleOfMovie of the movie: ");
                string movieTitle = Console.ReadLine();
                Movie movie = movieCollection.gettingMovie(movieTitle);
                if (movie != null)
                {
                    Console.WriteLine(movie);
                }
                else
                {
                    Console.WriteLine("This movie cannot be found in the system.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is an error, please try again: {ex.Message}");
            }
        }

        static void borrowTheMovie(MovieCollection movieCollection, Member member)
        {
            try
            {
                Console.Write("Enter the movie that you want to borrow: ");
                string title = Console.ReadLine();
                Movie movie = movieCollection.gettingMovie(title);
                if (movie != null && movie.gettingCopiesAvailable() > 0)
                {
                    if (member.movieBorrow(movie))
                    {
                        movie.decrementAvailableCopies();
                        movie.BorrowCountIncrement();  // Increment borrow count
                        Console.WriteLine("You have successfully borrowed the movie.");
                    }
                    else
                    {
                        Console.WriteLine("You cannot borrow more than five movies or duplicate movies.");
                    }
                }
                else
                {
                    Console.WriteLine("This movie is not available in the system.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is an error, please try again: {ex.Message}");
            }
        }

        static void returnTheMovie(MovieCollection movieCollection, Member member)
        {
            try
            {
                Console.Write("Please enter the movie to return: ");
                string title = Console.ReadLine();
                Movie movie = movieCollection.gettingMovie(title);
                if (movie != null)
                {
                    if (member.returningMovie(movie))
                    {
                        movie.incrementAvailableCopies();
                        Console.WriteLine("The movie has been returned successfully.");
                    }
                    else
                    {
                        Console.WriteLine("The member has not borrowed this movie.");
                    }
                }
                else
                {
                    Console.WriteLine("This movie cannot be found in the system.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is an error, please try again: {ex.Message}");
            }
        }

        static void listMoviesBorrowed(Member member)
        {
            try
            {
                for (int n = 0; n < member.getBorrowedMoviesCount(); n++)
                {
                    if (member.GetMoviesBorrowed()[n] != null)
                    {
                        Console.WriteLine(member.GetMoviesBorrowed()[n]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is an error, please try again: {ex.Message}");
            }
        }

        //displaying the top three borrowed movies 
        static void displayTopThreeBorrowedMovies(MovieCollection movieCollection)
        {
            try
            {
                Movie[] theTopThreeMovies = movieCollection.gettingTopThreeBorrowedMovies();
                if (theTopThreeMovies.Length == 0)
                {
                    Console.WriteLine("There is no such movie in the system.");
                    return;
                }

                Console.WriteLine("The top three borrowed movies are:");
                for (int n = 0; n < theTopThreeMovies.Length; n++)
                {
                    if (theTopThreeMovies[n] != null)
                    {
                        Console.WriteLine($"{theTopThreeMovies[n].gettingTitle()} has been borrowed {theTopThreeMovies[n].gettingBorrowCount()} times");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is an error, please try again: {ex.Message}");
            }
        }
    }
}
