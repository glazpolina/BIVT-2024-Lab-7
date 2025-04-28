using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }

            public void SetPlace(int place)
            {
                if (_place < 0) return;
                if (_place != 0) return;//in case this place was already taken
                _place = place;
            }
            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {Place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsman;
            private int _counter;
            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsman;
            ////77777

            public int SummaryScore
            {
                get
                {
                    if (_sportsman == null || _sportsman.Length == 0) return 0;
                    int total = 0;
                    for (int i = 0; i < _sportsman.Length; i++)
                    {
                        if (_sportsman[i] != null)
                        {
                            switch (_sportsman[i].Place)
                            {
                                case 1: total += 5; break;
                                case 2: total += 4; break;
                                case 3: total += 3; break;
                                case 4: total += 2; break;
                                case 5: total += 1; break;
                                default: break;
                            }
                        }
                    }
                    return total;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_sportsman == null) return 0;
                    int best = 18;
                    for (int i = 0; i < _sportsman.Length; i++)
                    {
                        if (_sportsman[i] != null)
                        {
                            if ((_sportsman[i].Place < best) && (_sportsman[i].Place > 0)) { best = _sportsman[i].Place; }
                        }
                    }
                    return best;
                }
            }
            protected abstract double GetTeamStrength();
            public static Team GetChampion(Team[] teams)
            {
                //чемпион среди и тех и тех, возвращает команду-чемпиона
                if (teams == null || teams.Length == 0) return null;
                Team champ = null;//champion team
                double max_of_strength;
                //double max_of_strength = teams[0].GetTeamStrength();//doesn't work in case teams[0] is nonexistent
                if (teams[0] != null) max_of_strength = teams[0].GetTeamStrength();
                else max_of_strength = double.MinValue;

                foreach (Team team in teams)
                {
                    if (team == null) continue;

                    double strength_of_a_team = team.GetTeamStrength();

                    if (strength_of_a_team > max_of_strength) 
                    { 
                        max_of_strength = strength_of_a_team; 
                        champ = team; 
                    }
                }
                return champ;
            }

            public Team(string name)
            {
                _name = name;
                _sportsman = new Sportsman[6];
                _counter = 0;
            }

            public void Add(Sportsman newsportsman)
            {
                if (_sportsman == null || newsportsman==null) return;///
                if (_counter < _sportsman.Length)
                {
                    _sportsman[_counter++] = newsportsman;
                }
                else return;///
            }

            public void Add(Sportsman[] newsportsman)
            {
                if (_sportsman.Length == 0 || newsportsman.Length == 0 || _counter >= _sportsman.Length || _sportsman == null || newsportsman == null) return;
                int k_new = 0;
                while ((_counter < _sportsman.Length) && (k_new < newsportsman.Length))
                {
                    _sportsman[_counter] = newsportsman[k_new];
                    _counter++;
                    k_new++;
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams.Length == 0 || teams == null) return;
                for (int i = 1, j = 2; i < teams.Length;)
                {
                    if (i == 0 || teams[i - 1].SummaryScore > teams[i].SummaryScore || (teams[i - 1].SummaryScore == teams[i].SummaryScore) && teams[i - 1].TopPlace <= teams[i].TopPlace) { i = j; j++; }
                    else
                    {
                        Team t = teams[i];
                        teams[i] = teams[i - 1];
                        teams[i - 1] = t;
                        i--;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Команда {Name} с результатом {SummaryScore} заняла {TopPlace} место");
            }
        }
        public class ManTeam : Team///
        {
            
            public ManTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                //сила-это 100 умн на среднее значение мест
                if (Sportsmen == null || Sportsmen.Length == 0) return 0;
                double strength = 0;
                double sum_of_places = 0, number_of_participants = 0;
                
                foreach (Sportsman sportsman in Sportsmen)
                {
                    if (sportsman.Place > 0 && sportsman != null)
                    {
                        sum_of_places += sportsman.Place;
                       
                        number_of_participants++;

                    }
                }
                if (number_of_participants == 0) return 0;//그
                strength = 100 * (sum_of_places / number_of_participants);
                return strength;
            }
        }
        public class WomanTeam : Team///
        {
            public WomanTeam(string name) : base(name) { }
            protected override double GetTeamStrength()
            {
                ///сила-это 100 умн на (сумму мест) умн на (число участников) дел на (произведение их мест)
                if (Sportsmen == null || Sportsmen.Length == 0) return 0;
                double strength = 0;
                double sum_of_places = 0, number_of_participants = 0;
                double product_of_places = 1;
                foreach(Sportsman sportsman in Sportsmen)
                {
                    if (sportsman.Place > 0 && sportsman != null)
                    {
                        sum_of_places += sportsman.Place;
                        product_of_places *= sportsman.Place;
                        number_of_participants++;

                    }
                }
                strength = 100 * sum_of_places * number_of_participants / product_of_places;
                return strength;
            }
        }
    }

}
