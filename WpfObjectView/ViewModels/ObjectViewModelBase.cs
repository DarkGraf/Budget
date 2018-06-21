using Prism.Mvvm;
using System;
using System.Linq;
using WpfObjectView.Attributes;
using WpfObjectView.ViewModels.Interfaces;

namespace WpfObjectView.ViewModels
{
    public abstract class ObjectViewModelBase<T> : BindableBase, IRealKeyViewModel
        where T : class, new()
    {
        [VisibleInView(false)]
        public T Object { get; }

        // Может быть несколько различных доменных объектов представляющих один реальный объект,
        // т. е. размещенных в разных местах памяти. Соответственно ViewModel`и их представляющие
        // тем более будут разные. Данное поле должно идетифицировать именно реальный объект.
        [VisibleInView(false)]
        public abstract long RealKey { get; }

        public ObjectViewModelBase()
        {
            // Создание нового объекта на добавлении.
            Object = new T();

            InititalizeProperties();
        }

        public ObjectViewModelBase(T obj)
        {
            Object = obj ?? throw new ArgumentNullException(nameof(obj));
        }

        protected abstract string DisplayName { get; }

        public override string ToString()
        {
            return DisplayName;
        }

        private void InititalizeProperties()
        {
            // Если есть свойства, которые наследуют от ObjectViewModelBase<>, 
            // то создадим для них значения по умолчанию. Так как это свойство 
            // также является наследником данного класса, оно проинициализурует 
            // свои вложенные свойства. Т. е. инициализация выполнится рекурсивно.

            var props = GetType().GetProperties().Where(p => IsSubclassOfRawGeneric(typeof(ObjectViewModelBase<>), p.PropertyType)).ToArray();
            foreach (var prop in props)
            {
                var o = Activator.CreateInstance(prop.PropertyType);
                prop.SetValue(this, o);
            }
        }

        private bool IsSubclassOfRawGeneric(Type rawGeneric, Type subclass)
        {
            while (subclass != typeof(object))
            {
                var cur = subclass.IsGenericType ? subclass.GetGenericTypeDefinition() : subclass;
                if (rawGeneric == cur)
                {
                    return true;
                }
                subclass = subclass.BaseType;
            }
            return false;
        }
    }
}