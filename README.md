<h1>Community System Software Library</h1>


<h2>Description</h2>
Community System Software Library is a C# console application for community library, integrating hash table.
It has features for managing DVDs and members, including adding/removing records with robust error handling for invalid operations, ensuring accurate transaction management.
This project streamlines top 3 DVD borrowing algorithm.
<br />


<h2>Languages Used</h2>
- <b>C#</b>

<h2> Hashing Technique Used</h2>
- <b>Double Hashing: Double hashing is used in the code, which is a more efficient way to handle collisions in the hash table than linear and quadratic probing. This decision was made primarily to reduce the issues related to clustering, which are more common with linear and quadratic probing techniques. Because clustering results in lengthy probing sequences when several entries cluster around a single index, it can dramatically reduce performance by raising the average time it takes to insert or locate an entry. Double hashing disperses the entries more equally throughout the hash table by utilizing a second hash function to determine the step size for each probe.
Due to its ability to reduce the likelihood of forming big clusters, the uniform distribution produced by double hashing is especially beneficial in average circumstances where fewer probes are needed. This algorithm uses two hash functions: one to find the first slot, and another to figure out how far the next probe should be spaced out if the first slot is taken. This makes sure that the hash table continues to function well even as it fills up and drastically lowers the likelihood of secondary clustering, which is prevalent in quadratic probing. The overall performance of actions such as inserting, deleting, and retrieving movies from the collection is improved by double hashing, which keeps the number of probes per operation lower and more constant.
</b>

<h2>Program walk-through:</h2>

<p align="center">
First Output and Error Handling: <br/>
<img src="https://github.com/user-attachments/assets/850efb35-a3e4-40a0-89a2-6abc6d9f756d" height="80%" width="80%" alt="Community"/>
<img src="https://github.com/user-attachments/assets/3188980c-8e45-4509-b3cb-f4b5793e5c82" height="80%" width="80%" alt="Community"/>
<br />
<br />

Add DVDs and manage error handling: <br/>
<img src="https://github.com/user-attachments/assets/5143d6df-5898-44a5-a4aa-286b0c28a02c" height="80%" width="80%" alt="Community"/>
<img src="https://github.com/user-attachments/assets/5075a228-3f92-4841-9d50-06b768840b35" height="80%" width="80%" alt="Community"/>
<img src="https://github.com/user-attachments/assets/3421419d-81e4-4217-adfe-0c1bb3ee155e" height="80%" width="80%" alt="Community"/>
<br />
<br />


