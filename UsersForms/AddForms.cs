using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UsersForms
{
    public partial class AddForms : Form
    {
        //Событие для передачи данных
        public static bool admChange = false;
        public event EventHandler<UserEventArgs> sendDataFormEvent;
        public AddForms()            

        {            
            InitializeComponent();

        }

        private void buttonAddOk_Click(object sender, EventArgs e)
        {
            //создаем объект типа UserClass
            UserClass temp = new UserClass(textBoxName.Text, textBoxPass.Text);
            if (checkBoxAdmin.Checked)
                temp.aUser = true;
            else
                temp.aUser = false;
            temp.dUser = false;
            //Генерируем событие с именованным аргументом
            //в класс аргумента передаем созданный объект
            if (sendDataFormEvent != null)
                sendDataFormEvent(this, new UserEventArgs(temp));

            //Закрываем форму
            this.Close();
        }

        private void checkBoxAdmin_CheckedChanged(object sender, EventArgs e)
        {
            //if (admChange) admChange = false;
            //else admChange = true;
        }
    }
}
