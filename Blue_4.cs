using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;

            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;
                    int[] copscores = new int[_scores.Length];
                    for (int i = 0; i < copscores.Length; i++)
                    {
                        copscores[i] = _scores[i];
                    }
                    return copscores;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;
                    int total = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        total += _scores[i];
                    }
                    return total;
                }
            }

            public Team(string name)
            {
                _name = name;
                _scores = new int[0];

            }

            public void PlayMatch(int result)
            {
                if (_scores == null) return;
                int[] new_scores = new int[_scores.Length + 1];
                for (int i = 0; i < _scores.Length; i++)
                {
                    new_scores[i] = _scores[i];
                }
                new_scores[new_scores.Length - 1] = result;
                _scores = new_scores;
            }

            public void Print()
            {
                Console.WriteLine($"{Name}: {TotalScore}");
            }
        }
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        }
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        }

        public class Group
        {
            private string _name;
            private ManTeam[] _man;//
            private WomanTeam[] _woman;//
            private int _man_count;
            private int _woman_count;

            public string Name => _name;
            public ManTeam[] ManTeams => _man;//
            public WomanTeam[] WomanTeams => _woman;//

            public Group(string name)
            {
                _name = name;
                _man = new ManTeam[12];//
                _woman= new WomanTeam[12];//
                _man_count = 0;
                _woman_count = 0;
            }

            public void Add(Team team)
            {
                if (_man == null || _woman == null) return;
                if (_man_count >= 12) return;
                if (_woman_count >= 12) return;
                if (team is ManTeam tea) { _man[_man_count++] = tea; }
                else if (team is WomanTeam te) { _woman[_woman_count++] = te; }
            }

            public void Add(Team[] teams)///
            {
                if (_man == null || _woman==null) return;
                //
                foreach(Team t in teams)
                {
                    Add(t);
                }
            }

            public void Sort()
            {
               //
               Sortsep(_man);
               Sortsep(_woman);
            }
            private void Sortsep(Team[] t)///sort mass separately
            {
                if (t == null) return;
                for (int i = 1, j = 2; i < t.Length;)
                {
                    if (i == 0 || t[i].TotalScore <= t[i - 1].TotalScore) { i = j; j++; }
                    else
                    {
                        var temp = t[i];
                        t[i] = t[i - 1];
                        t[i - 1] = temp;
                        i--;
                    }
                }
            }

            public static Group Merge(Group group1, Group group2, int size)///соединять отдельно м и ж
            {
                Group finale = new Group("Финалисты");
                Team[] team_of_man = Merge_an_Array(group1._man, group2._man, size);
                Team[] team_of_woman = Merge_an_Array(group1._woman, group2._woman, size);
                finale.Add(team_of_man);
                finale.Add(team_of_woman);
                return finale;
            }
            private static Team[] Merge_an_Array(Team[] group1, Team[] group2,int size)
            {
                Team[] merged_array = new Team[size];
                int i = 0, j = 0;
                int q = 0;
                while (i < size / 2 && j < size / 2)////
                {
                    if (group1[i].TotalScore >= group2[j].TotalScore)
                    {
                        merged_array[q]=group1[i];
                        q++;
                        i++;
                    }
                    else
                    {
                        merged_array[q]=group2[j];
                        q++;
                        j++;
                    }
                }

                while (i < size / 2)///
                {
                    merged_array[q]=group1[i];
                    q++;
                    i++;

                }
                while (j < size / 2)///
                {
                    merged_array[q]=group2[j];
                    q++;
                    j++;

                }
                return merged_array;
            }
            public void Print()
            {
                Console.WriteLine(_name);
                for (int i = 0; i < _man_count; i++)
                {
                    _man[i].Print();
                }
                Console.WriteLine();
                for (int i = 0; i < _woman_count; i++)
                {
                    _woman[i].Print();
                }
            }
        }
    }
}
