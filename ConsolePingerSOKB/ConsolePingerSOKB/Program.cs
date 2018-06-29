using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Data;
using System.Data.OracleClient;


namespace ConsolePinger
{
    class Program
    {
        static void Main(string[] args)
        {
            // PING
            Console.ForegroundColor = ConsoleColor.Green;
            string s1;
            string s2;
            Ping Pinger = new Ping();
            PingReply Reply = Pinger.Send("192.168.21.99");
            Console.WriteLine("IP: {0} Status:{1}", Reply.Address.ToString(), Reply.Status);
            
            // Connet Oracle and select table
            Console.ForegroundColor = ConsoleColor.Yellow;
            string connectionString = "Data Source=asu;User Id=asu;Password=asu;Integrated Security=no;";
            string queryString = "select fc_name, fc_host, display_name, ip_adress from asu.ts_analizator";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = connection.CreateCommand();
                command.CommandText = queryString;
                try
                {
                    connection.Open();
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("-----------------------------------------------------------------");
                        Console.WriteLine("\t{0}\t{1}\t{2}\t{3}", reader[0], reader[1], reader[2], reader[3]);

                        s1 = Convert.ToString(reader[1]);
                        if (s1 != string.Empty)
                        {
                            PingReply Reply1 = Pinger.Send(s1);
                            Console.WriteLine("IP: {0} Status:{1}", Reply1.Address.ToString(), Reply1.Status);
                        }
                        s2 = Convert.ToString(reader[3]);
                        if (s2 != string.Empty)
                        {
                            PingReply Reply2 = Pinger.Send(s2);
                            Console.WriteLine("IP: {0} Status:{1}", Reply2.Address.ToString(), Reply2.Status);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                connection.Close();
            }
            // Sample ping list
            //List IPs = new List();
            //IPs.Add("10.1.1.12");
            //IPs.Add("10.1.1.15");
            //IPs.Add("OAOkomp1");
            //IPs.Add("192.168.1.1");
            //IPs.Add("www.google.com");
            //IPs.Add("www.amazon.com");
            //System.Net.NetworkInformation.Ping Pinger =
            //    new System.Net.NetworkInformation.Ping();
            //foreach (string ip in IPs)
            //{
            //    try
            //    {
            //        System.Net.NetworkInformation.PingReply Reply = Pinger.Send(ip);
            //        listBox1.Items.Add(String.Format("Ping {0}: {1}", ip, Reply.Status));
            //    }
            //    catch (Exception) { }
            //}

            //Данные примеры позволят вам определить состояние компьютеров в вашей локальной сети или в интернете. 
            //Ping класс предоставляет несколько опций, которые можно использовать для управления запросами, 
            //а также возможность отправки асинхронных запросов.
            //Console.WriteLine(" Status {0}", Reply.Status);

            //Wait ENTER
            Console.ReadLine();
        }
    }
}