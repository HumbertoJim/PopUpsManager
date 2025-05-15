using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    namespace PopUps
    {
        namespace Exceptions
        {
            public class PopUpException : Exception
            {
                public PopUpException() { }

                public PopUpException(string message) : base(message) { }

                public PopUpException(string message, Exception innerException) : base(message, innerException) { }
            }
        }
    }
}
