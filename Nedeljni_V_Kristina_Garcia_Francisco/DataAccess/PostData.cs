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
        /// Get all data about users from the database
        /// </summary>
        /// <returns>The list of all users</returns>
        public List<tblPost> GetAllPosts()
        {
            try
            {
                using (BetweenUsDBEntities context = new BetweenUsDBEntities())
                {
                    List<tblPost> list = new List<tblPost>();
                    list = (from x in context.tblPosts select x).ToList();
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
