using System;

namespace UsersForms
{
    //public класс, наследник от EventArgs
    //Используется как аргумент для передачи данных
    //в событиях
    public class UserEventArgs : EventArgs
    {
        //public (readonly) поле класса
        public UserClass SendingUser;
        //Конструктор класса с параметром
        public UserEventArgs(UserClass User)
        {
            // передает объект типа UserClass
            SendingUser = User;
        }
    }
}
