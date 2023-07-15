![Alt text](https://gitlab.com/project-one-group-five/forum-system/-/raw/main/ImagesForREADME/telerik.PNG)

Blockie Forum System
====================


>Blockie is a forum system designed for cryptocurrency enthusiasts. It empowers users to actively engage in discussions by creating posts and comments related to various crypto topics. Users have the freedom to express their opinions by liking or disliking posts and comments. Additionally, Blockie offers a comprehensive search functionality, enabling users to easily find specific users or posts of interest.
 This project is developed using the ASP.NET Core 6 framework and encompasses a public API and a User Interface built with MVC, ensuring a seamless and intuitive user experience.

## Project Description
### Areas
* **Public part** -  accessible without authentication
* **Private part** - available for registered users only
* **Administrative part** - available for administrators only

#### Home Page
* The home page of our forum system showcases real-time active user count, total post count, and tables featuring the top ten most commented and created posts, providing visitors with a glimpse into the vibrant community and engaging discussions.

![Alt text](https://gitlab.com/project-one-group-five/forum-system/-/raw/main/ImagesForREADME/homepage.jpeg)


* **Additionally, anonymous visitors have the privilege to read and view posts, enabling them to access valuable information. However, they are restricted from creating posts, commenting on existing posts, liking or disliking content, and accessing other users' profiles.**


![Alt text](https://gitlab.com/project-one-group-five/forum-system/-/raw/main/ImagesForREADME/post.png)

#### Login Page
* The login page provides visitors with the opportunity to access the full potential of the website by logging in with their accounts, unlocking various features and functionalities.

![Alt text](https://gitlab.com/project-one-group-five/forum-system/-/raw/main/ImagesForREADME/login.png)


### Register Page
* Visitors can register for the system and unlock the full potential o the website.

### View All Posts
* In this forum system, every visitor of the website is provided with the ability to access and view all created posts. Additionally, visitors have the option to utilize the search feature to refine their browsing experience by searching for posts based on the following criteria:

     * Title - By entering specific keywords or phrases into the search bar, all posts that contain the input in their titles will be displayed. 
     * Content – all posts that contain the input in their content will be displayed.
     * Created by - By entering a specific username as input, all posts that match the input and were created by the corresponding user will be displayed.
     * Tag - By entering relevant keywords or phrases as input, all posts that contain matching tags will be displayed. 
	 * Start/End Date - By selecting a start date and an end date, the system will retrieve and display all posts that fall within that date range.
	 * Sort posts by:
	  	 * Title
		 * Comments
		 * Likes
		 * Date
	 *Order posts in either ascending or descending order.
### Main Search for NOT logged users
* Anonymous visitors are granted access to the search functionality available in the navigation bar. This feature allows them to search for posts based on specific keywords present in the post title, as well as filter results by relevant tags.

### Main Search for logged users
* Once a visitor is authenticated, they gain access to the main search feature, which enables them to search for other users based on specific keywords present in their profile title.

### Create Post 
* Logged-in users have the ability to create posts within the forum system and enhance their visibility by adding relevant tags.

### Edit Post
* Furthermore, logged-in users who are the creators of a post possess the capability to make edits to the post, including modifying the title, content, and adding or modifying tags associated with the post.

### Add Comment
* Logged-in users are provided with the privilege to actively engage with the community by adding comments to existing posts.




