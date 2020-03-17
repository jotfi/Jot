using Nista.Jottre.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nista.Jottre.Core
{
    public class Application
    {
        public readonly ViewModelController ViewModels;

        public Application()
        {
            ViewModels = new ViewModelController(this);
        }

        public virtual void Run()
        {

        }
    }
}
