using Nedeljni_V_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Nedeljni_V_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// class used to create the post CRUD structure of the application
    /// </summary>
    class PostData
    {
        /// <summary>
        /// Get all data about posts from the database
        /// </summary>
        /// <returns>The list of all posts</returns>
        public List<tblPost> GetAllPosts()
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    List<tblPost> list = new List<tblPost>();
                    list = (from x in context.tblPosts select x).ToList();
                    return list.OrderByDescending(x => x.DateOfPost).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get all data about users posts from the database
        /// </summary>
        /// <returns>The list of all users posts</returns>
        public List<tblPost> GetAllUserPosts(tblUser user)
        {
            try
            {
                List<tblPost> list = new List<tblPost>();
                List<tblPost> allPosts = GetAllPosts().ToList();

                for (int i = 0; i < allPosts.Count; i++)
                {
                    if (allPosts[i].UserID == user.UserID)
                    {
                        list.Add(allPosts[i]);
                    }
                }

                return list.OrderByDescending(x => x.DateOfPost).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get all post likes from the database
        /// </summary>
        /// <returns>The list of all post likes</returns>
        public List<tblPostLike> GetAllPostLikes()
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    List<tblPostLike> list = new List<tblPostLike>();
                    list = (from x in context.tblPostLikes select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get all post likes from a specific post
        /// </summary>
        /// <returns>The list of all post likes</returns>
        public List<tblPostLike> GetAllPostLikesFromPost(tblPost post)
        {
            List<tblPostLike> allPostLikes = GetAllPostLikes().ToList();
            List<tblPostLike> list = new List<tblPostLike>();

            for (int i = 0; i < allPostLikes.Count; i++)
            {
                if (allPostLikes[i].PostID == post.PostID)
                {
                    list.Add(allPostLikes[i]);
                }
            }
            return list;
        }

        /// <summary>
        /// Add a post to the database
        /// </summary>
        /// <param name="post">The post ID we are adding or editing</param>
        /// <returns>The new post</returns>
        public tblPost AddPost(tblPost post)
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    if (post.PostID == 0)
                    {
                        tblPost newPost = new tblPost
                        {
                            DateOfPost = DateTime.Now,
                            PostText = post.PostText,
                            NumberOfLikes = 0,
                            UserID = LoggedInUser.CurrentUser.UserID,
                        };

                        context.tblPosts.Add(newPost);
                        context.SaveChanges();
                        post.PostID = newPost.PostID;

                        return post;
                    }
                    else
                    {
                        tblPost postToEdit = (from ss in context.tblPosts where ss.PostID == post.PostID select ss).First();

                        // Increase number of likes in the post
                        postToEdit.NumberOfLikes = postToEdit.NumberOfLikes + 1;
                        post.NumberOfLikes = postToEdit.NumberOfLikes + 1;
                        postToEdit.PostID = post.PostID;
                        context.SaveChanges();

                        return post;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Add Like
        /// </summary>
        /// <param name="post">The post ID we are adding a like to/param>
        public void AddLike(tblPost post)
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    tblPostLike newPostLike = new tblPostLike
                    {
                        PostID = post.PostID,
                        UserID = LoggedInUser.CurrentUser.UserID,
                    };

                    context.tblPostLikes.Add(newPostLike);
                    context.SaveChanges();

                    // Update the number of likes in the post
                    AddPost(post);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Check if post was liked by user
        /// </summary>
        /// <param name="post">the post we are checking</param>
        /// <param name="user">the user</param>
        /// <returns>bool if already liked the post</returns>
        public bool IsPostLiked(tblPost post, tblUser user)
        {
            List<tblPostLike> allPostLikesForPost = GetAllPostLikesFromPost(post).ToList();

            for (int i = 0; i < allPostLikesForPost.Count; i++)
            {
                if (allPostLikesForPost[i].UserID == user.UserID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
