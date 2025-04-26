using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _out_min;

            public string Name => _name;
            public string Surname => _surname;
            public int[] Penalties
            {
                get
                {

                    if (_out_min == null) return null;//
                    int[] copiedarray = new int[_out_min.Length];
                    for (int i = 0; i < _out_min.Length; i++)
                    {
                        copiedarray[i] = _out_min[i];
                    }
                    return copiedarray;
                }
            }

            public int Total
            {
                get
                {
                    if (_out_min == null || _out_min.Length == 0) return 0;
                    int total = 0;
                    for (int i = 0; i < _out_min.Length; i++)
                    {
                        total += _out_min[i];
                    }
                    return total;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_out_min == null || _out_min.Length == 0) return false;//
                    for (int i = 0; i < _out_min.Length; i++)
                    {
                        if (_out_min[i] == 10) return true;
                    }
                    return false;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _out_min = new int[0];

            }

            public virtual void PlayMatch(int time)
            {
                if (_out_min == null) return;
                int[] new_penalty_min = new int[_out_min.Length + 1];

                for (int i = 0; i < new_penalty_min.Length - 1; i++)
                {
                    new_penalty_min[i] = _out_min[i];
                }
                new_penalty_min[new_penalty_min.Length - 1] = time;
                _out_min = new_penalty_min;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 1, j = 2; i < array.Length;)
                {
                    if (i == 0 || array[i].Total >= array[i - 1].Total) { i = j; j++; }
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
                Console.WriteLine($"{Name} {Surname}: {Total}  {IsExpelled}");
            }
        }

        public class BasketballPlayer : Participant
        {
            //игрок должен исключаться если у него больше 10 проц игр с 5 фолами или 
            //сумма фолов вдвое больше чем кол-во игр
            public override void PlayMatch(int number_of_falls)
            {
                if (number_of_falls <= 5 && number_of_falls >= 0) base.PlayMatch(number_of_falls);
                else return;//записывается кол-во фолов за матчи, фолы должны быть от 0 до 5
                
            }
            public BasketballPlayer(string name, string surname) : base(name, surname) { _out_min = new int[0]; }
            public override bool IsExpelled
            {
                get
                {
                    if (_out_min == null || _out_min.Length == 0) return false;//
                    int counter_of_matches_five_f = 0;
                    for (int i = 0; i < this.Penalties.Length; i++) 
                    { 
                        if (this.Penalties[i] >= 5) counter_of_matches_five_f++; 
                    }
                    if ((100*counter_of_matches_five_f/this.Penalties.Length) > 10) return true;//игрок должен исключаться если у него больше 10 проц игр с 5 фолами
                    if (this.Total > this.Penalties.Length * 2) return true;//сумма фолов вдвое больше чем кол-во игр
                    return false;
                }
            }

        }

        public class HockeyPlayer : Participant
        {
            //исключается если хотя бы в одном матче 10 мин штрафного времени или
            //его суммарное штрафное время больше,
            //чем 10% от суммарного штрафного времени всех хоккеистов,
            //разделенного на количество хоккеистов.

            private static int _count_of_hockey_players;//all hockey players 
            private static int _out_min_total;//sum of all of the penalty times of all players
            public HockeyPlayer(string name,string surname) : base(name, surname)
            {
                _out_min= new int[0];
                _count_of_hockey_players++;//counting all of the hockey players
            }
            public override void PlayMatch(int time)
            {
                if (_out_min == null) return;
                if (time < 0||time>10) return;
                base.PlayMatch(time);
                _out_min_total += time;
            }
            public override bool IsExpelled 
            { 
                get 
                {
                    if (_out_min == null) return false;
                    
                    for (int i = 0; i < this.Penalties.Length; i++)
                    {
                        if (this.Penalties[i] >= 10) return true;//cancelling those with any penalty over 10 min
                    }
                   
                    if (this.Total >((0.1*_out_min_total)/_count_of_hockey_players)) return true;
                    return false;
                } 
            }
        }
    }
}
