//Anthony Franklin afranklin18@cnm.edu
//FranklinP8
//04/23/2022
//DBManager

using System.Configuration;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System;
using System.Linq;

namespace FranklinP8
{
    public class DBManager
    {
        string connStr = ConfigurationManager.ConnectionStrings["FlashCardDB"].ConnectionString;
        public DBManager() { }

        /// <summary>
        /// Add Card Object to DB
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card) 
        {
            //Add item to database
            string insStr = "INSERT INTO Card (Title, Question, Answer, NumRight, NumWrong) VALUES(@Title, @Question, @Answer, 0, 0)";
            using SqlConnection conn = new SqlConnection(connStr);
            SqlCommand insCmd = new SqlCommand(insStr, conn);
            insCmd.Parameters.AddWithValue("Title", card.Title);
            insCmd.Parameters.AddWithValue("Question", card.Question);
            insCmd.Parameters.AddWithValue("Answer", card.Answer);
            conn.Open();
            insCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Populate BindingList<Card> from FlashCard DB
        /// </summary>
        /// <param name="cards"></param>
        public void GetCards(BindingList<Card> cards) 
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand selectCmd = new SqlCommand("SELECT * FROM Card", conn);
                conn.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();
                cards.Clear();
                while(dr.Read())
                {
                    Card card = new Card();
                    card.CardID = Convert.ToInt32(dr["CardId"]);
                    card.Title = dr["Title"].ToString();
                    card.Question = dr["Question"].ToString();
                    card.Answer = dr["Answer"].ToString();
                    card.NumRight = Convert.ToInt32(dr["NumRight"]);
                    card.NumWrong = Convert.ToInt32(dr["NumWrong"]);
                    cards.Add(card);
                }
            }
        }

        /// <summary>
        /// Delete Card Object from DB
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCard(Card card) 
        {
            string delStr = "DELETE FROM Card WHERE CardID = @CardID";
            using SqlConnection conn = new SqlConnection(connStr);
            SqlCommand delCmd = new SqlCommand(delStr, conn);
            delCmd.Parameters.AddWithValue("CardID", card.CardID);
            conn.Open();
            delCmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Update Flash Card Values
        /// </summary>
        /// <param name="card"></param>
        public void Update(Card card) 
        {
            string updStr = "UPDATE Card SET Title=@Title, Question = @Question, Answer = @Answer, NumRight= @NumRight, NumWrong= @NumWrong WHERE CardID=@CardID";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand updCmd = new SqlCommand(updStr, conn);
                updCmd.Parameters.AddWithValue("Title", card.Title);
                updCmd.Parameters.AddWithValue("Question", card.Question);
                updCmd.Parameters.AddWithValue("Answer", card.Answer);
                updCmd.Parameters.AddWithValue("NumRight", card.NumRight);
                updCmd.Parameters.AddWithValue("NumWrong", card.NumWrong);
                updCmd.Parameters.AddWithValue("CardID", card.CardID);
                conn.Open();
                updCmd.ExecuteNonQuery();
            }
        }

    }
}
