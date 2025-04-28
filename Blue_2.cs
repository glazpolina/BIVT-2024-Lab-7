using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
        public abstract class WaterJump
        {
            private string _name;
            private int _money;
            private Participant[] _rivals;
            

            public string Name => _name;
            public int Bank => _money;
            public Participant[] Participants => _rivals;
            
            public abstract double[] Prize { get; }

            public WaterJump(string name, int money)
            {
                _name = name;
                _money = money;
                _rivals = new Participant[0];
                
            }

            ////
            public void Add(Participant rival)//rewritten
            {
                if (_rivals == null) return;//
                var newarrayrivals=new Participant[_rivals.Length+1];
                for (int i = 0; i < _rivals.Length; i++) 
                { 
                    newarrayrivals[i]=_rivals[i];
                }
                newarrayrivals[_rivals.Length] = rival;
                _rivals=newarrayrivals;
                
            }
            public void Add(Participant[] rivals) //rewritten
            {
                if (rivals == null || _rivals == null) return;
                foreach ( var rival in rivals ) {Add(rival);}
                
            }
          
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string Name, int money):base(Name, money) { }
            public override double[] Prize
            {
                get 
                {
                    if (this.Participants == null) return null;//
                    if (this.Participants.Length < 3) return null;//
                    double[]money_for_prizetakers=new double[3];
                    money_for_prizetakers[0] = 0.5 * this.Bank;//1st place
                    money_for_prizetakers[1] = 0.3 * this.Bank;//2nd place
                    money_for_prizetakers[2] = 0.2 * this.Bank;//3rd place
                    return money_for_prizetakers;
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string Name, int money) : base(Name, money) { }
            public override double[] Prize
            {
                get
                {
                    if (this.Participants == null) return null;//
                    if (this.Participants.Length < 3) return null;//
                    int middle = (this.Participants.Length) / 2;
                    if (middle >= 10) { middle = 10; }//middle indicates how many people i'm giving money to
                    double[] money_for_prizetakers = new double[middle];
                    for(int i = 0; i < middle; i++)
                    {
                        money_for_prizetakers[i] = this.Bank * 20.0/ middle / 100;
                    }
                    money_for_prizetakers[0] += 0.4 * this.Bank;//additional to 1st
                    money_for_prizetakers[1] += 0.25 * this.Bank;//additional to 2nd
                    money_for_prizetakers[2] += 0.15 * this.Bank;//additional to 3rd
                    return money_for_prizetakers;
                }
            }

        }

        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _counter;

            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] copiedarrayofmarks = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            copiedarrayofmarks[i, j] = _marks[i, j];
                        }
                    }
                    return copiedarrayofmarks;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;
                    int total = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            total += _marks[i, j];
                        }
                    }
                    return total;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _counter = 0;
            }

            public void Jump(int[] result)
            {
                if (_marks == null || _marks.GetLength(0) == 0) return;
                if (result.Length == 0 || result == null) return;
                if (_counter > 1) return;
                for (int i = 0; i < 5; i++)
                {
                    _marks[_counter, i] = result[i];
                }
                _counter++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 1, j = 2; i < array.Length;)
                {
                    if (i == 0 || array[i].TotalScore <= array[i - 1].TotalScore) { i = j; j++; }
                    else
                    {
                        var temp = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = temp;
                        i--;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {TotalScore}");
            }
        }

    }
}
