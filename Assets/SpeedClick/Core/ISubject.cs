using System.Collections;
using System.Collections.Generic;

namespace Alisson.Core
{

    public interface ISubject<T> where T: class
    {
        void Notify();
        IList<IObserver<T>> Observers { get; }
        void Subscribe(IObserver<T> observer);
        void Unsubscribe(IObserver<T> observer);
    }

}
