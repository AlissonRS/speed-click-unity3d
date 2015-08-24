using System.Collections;

namespace Alisson.Core
{

    public interface IObserver<T> where T : class 
    {
        void UpdateObserver(T sub);

        T Element { get; set; }
    }

}
