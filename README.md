Community management system to manage movie DVDs using double hashing technique.


ALGORITHM displayTopThreeBorrowedMovies
//Input: movies [] (Hash table array from MovieCollection)
//Output: The top three borrowed movie DVDs title and borrow counts is 
displayed
PROGRAM Community Library System
FUNCTION displayTopThreeBorrowedMovies():
 DECLARE allMoviesInSystem AS ARRAY OF Movie
 DECLARE moviesSorted AS ARRAY OF Movie
 // Collect all movies which are non-null from the hash table
 FOR each index IN movies
 IF movies[index] IS NOT NULL AND occupied[index] IS TRUE
 ADD movies[index] TO allMoviesInSystem
 END IF
 END FOR
 // Sorting allMoviesInSystem by countingBorrowOfMovie in descending order
 SORT allMoviesInSystem BY countingBorrowOfMovie DESCENDING INTO 
moviesSorted
 // No. of movies that should be returned 
 DECLARE noOfTopMovies AS MINIMUM (3, LENGTH (moviesSorted))
 // Outcome based on the no. of top movies
 DECLARE result AS ARRAY OF Movie WITH size noOfTopMovies
 FOR n FROM 0 TO noOfTopMovies - 1
 finalresult[n] = moviesSorted[n]
 END FOR
 RETURN finalresult
END FUNCTION
