using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using System.Reactive;
using System.Text.RegularExpressions;
using System.IO;

namespace lab5.ViewModels {
    public class MainWindowViewModel : ViewModelBase {
        string _Text;
        public string Text {
            get => _Text;
            set {
                this.RaiseAndSetIfChanged(ref _Text, value);
            }
        }

        string _regex;
        public string Pattern
        {
            get => _regex;
            set {
                this.RaiseAndSetIfChanged(ref _regex, value);
            }
        }

        string _answer;
        public string Answer {
            get => _answer;
            set {
                this.RaiseAndSetIfChanged(ref _answer, value);
            }
        }

        public MainWindowViewModel() {
            this.WhenAnyValue(x => x.Text, x => x.Pattern).Subscribe(text => RegexCalculate(Text));
            
        }

        void RegexCalculate(string text) {
            if (Pattern == null || text == null){
                return;
            }
            Regex regex = new Regex(Pattern);
            var matches = regex.Matches(text);
            Answer = "";
            foreach(Match match in matches) {
                Answer += $"{match.Value}\n";
            }
        }

        public void SaveFile(string path) {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(Answer);
            }
        }

        public void LoadFile(string path) {
            if(File.Exists(path)) {
                using(StreamReader read = File.OpenText(path)) {
                    string res = ""; string str;
                    while((str=read.ReadLine())!=null) {
                        res += str+' ';
                    }
                    Text = res;
                }
            }
        }
    }
}
