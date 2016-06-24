using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsDBDemo
{
    public partial class Form1 : Form
    {
        //defined a NEW LIST of "listClients" from the database of  the "Client" table
        List<Client> listClients = new List<Client>();

        int clientListIndex = 0;

        public Form1()
        {
            InitializeComponent();
        }

        //this METHOD FOR DISPLAY the client database in the chosen text boxes/elements within the forms
        private void DisplayClientList(int customerId)
        {
            //if the client list which shows the client ID is not Null, then do this...
            if (listClients[customerId] != null)
            {
                textBox1.Text = listClients[customerId].Name;
                //to convert the age int to a string, you must input brackets and then .ToString()
                textBox2.Text = (listClients[customerId].Age).ToString();
                textBox3.Text = listClients[customerId].Address;
            }
        }

        //this is the button that will START THE DISPLAY of the clients from the database
        private void button4_Click(object sender, EventArgs e)
        {
            //the "using" keyword is used to access a database
            //defines a new instance of the object (the database)
            using (AbdulClientsEntities myEntities = new AbdulClientsEntities())
            {
                //the var word means a variable that will work regardles of what type it is
                //defines the variable name
                //"from" every client "cL" in the database, "select" a client "cL".
                var clients = from cL in myEntities.Clients
                              select cL;
                //^^ the statement above is the LINQ statement (Language integrated Query)

                //the list of clients defined at the top of the page is set to the client database via the myEntities name
                listClients = clients.ToList();
            }
            DisplayClientList(clientListIndex);
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;


        }

        //this is the button to press to get the NEXT Client "cL" in the CLient List (listClient)
        private void button1_Click(object sender, EventArgs e)
        {
            //the clientListIndex incremements by one each time the button is pressed, and the if 
            //statement is involved in the incremements in this button
            clientListIndex++;
            //if the list index int is bigger or equal to the list count (array sort of), then reset the client list index to 0
            if (clientListIndex >= listClients.Count)
            {
                clientListIndex = 0;
            }
            //with every time the button is pressed another client is shown in the text boxes via the "display Clients" method
            DisplayClientList(clientListIndex);
        }

        //this is the button that will access that PREVIOUSLY shown client in the database 
        private void button2_Click(object sender, EventArgs e)
        {
            //decrement the client list index by 1 each time the button is pressed
            clientListIndex--;

            //if statment, if the client list index is smaller than 0..
            //then we equal the index to the client list, plus a minus 1
            if (clientListIndex < 0)
            {
                clientListIndex = listClients.Count - 1;
            }

            //display the client list involved in this button
            DisplayClientList(clientListIndex);
        }

        //this button is to ADD the clients into the database, given that the form is still open for the session
        private void button5_Click(object sender, EventArgs e)
        {

            using (var myEntities = new AbdulClientsEntities())
            {
                //a new  Client object in the Client table called "addedClient"
                Client addedClient = new Client();
                //sets the input feilds of the database to the input feilds of the windows forms
                addedClient.Name = textBox1.Text;
                addedClient.Age = Convert.ToInt32(textBox2.Text);
                addedClient.Address = textBox3.Text;

                //this is to add the client details to the existing list of clients,
                //but not the overall database even after session finished
                myEntities.Clients.Add(addedClient);
                myEntities.SaveChanges();


            }
        }


        //the following button is used to DELETE clients form the database

        

    }
}
