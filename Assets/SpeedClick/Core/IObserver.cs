using System.Collections;

namespace Alisson.Core
{

    public interface IObserver<T> where T : class 
    {
        void ReceiveSubjectNotification(T sub);

        //T Element { get; set; }
    }

}
