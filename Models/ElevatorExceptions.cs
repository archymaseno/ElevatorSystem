using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystem.Models
{
    public class ElevatorExceptions : Exception
    {
        public ElevatorExceptions() { } 
        public ElevatorExceptions (string message) : base(message) { }  
        public ElevatorExceptions(string message, Exception inner) : base(message, inner) { }        
        public ElevatorExceptions(SerializationInfo info, StreamingContext context) : base(info, context) { }
       
    }

    public class ElevatorCapcityExceptions: ElevatorExceptions
    {
        public ElevatorCapcityExceptions(string message):base(message)
        {
                
        }
    }
}
