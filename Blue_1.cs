using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            private string _name;
            protected int _votes;
            public string Name => _name;
            public int Votes => _votes;

            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }
            public virtual int CountVotes(Response[] responses)
            {
                int k = 0;
                if (responses.Length == 0 || responses == null) return 0;
                foreach (Response response in responses)
                {
                    if ((response.Name == _name)) { k++; }
                }
                _votes = k;
                return _votes;
            }

            public virtual void Print()
            {
                Console.WriteLine($"{Name} {Votes}");
            }
        }

        public class HumanResponse : Response
        {
           
            private string _surname;
            public string Surname => _surname;
            public HumanResponse(string name, string surname) : base(name) 
            {
               
                _surname = surname;
                _votes = 0;
            }

            public override int CountVotes(Response[] responses) 
            {
                int k = 0;
                if (responses.Length == 0 || responses == null) return 0;
                foreach (Response response in responses)
                {
                    if (response==null) continue;
                    HumanResponse man = response as HumanResponse;
                    if ((response.Name == this.Name)&&(man.Surname==_surname)) { k++; }
                }
                _votes = k;
                return _votes;
            }

            public override void Print() 
            {
                Console.WriteLine($"{Name} {_surname} {_votes}");
            }
        }
    }
}
