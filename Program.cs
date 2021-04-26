using System.Collections.Generic;
using System.Linq;
using System;

namespace text_justify
{
    class Program
    {
        static void Main(string[] args)
        {
          //Console.WriteLine(Justify(" ",1));
          //Console.WriteLine(Justify(@"Lorem  ipsum  dolor  sit amet,consectetur  adipiscing  elit.Vestibulum  sagittis  dolormauris,  at  elementum  ligula tempor  eget.  In quis rhoncus
          //   nunc, at  aliquet orci. Fusce
          //   at dolor sit amet felissuscipit   tristique.   Nam  a
          //   imperdiet tellus.  Nulla  euvestibulum urna. Vivamus
          //       tincidunt suscipit  enim, necultrices nisi  volutpat  ac.
           //    Maecenas sit amet lacinia sed  quam  vel  risus faucibus
           //   euismod. suspendisse  rhoncus
           //   rhoncus  felis  at  fermentum. donec lorem magna, ultricies a
           //   nunc  sit  amet, blanditx fringilla  nunc. in vestibulum
           //   lacus, ut elementum  justo
           //   nulla et dolor. ",30));
          string text = "";
          Console.WriteLine("Please write your string : ");
          text = Console.ReadLine();
          int len = 0;
          Console.WriteLine("Enter Length : ");
          int.TryParse(Console.ReadLine(),out len);
          Console.WriteLine(Justify(text,len));
          Console.ReadKey();
        }

        public static string Justify(string str, int len)
        {
          if (str == null) return string.Empty;
          str += " ";
          string finall = string.Empty;
          var splited = SplitByWord(str);
          int countedWords = 0;
          for (int i = 0 ; i < splited.Count() ; i++)
          {
            if (countedWords == splited.Count()) break;
            var offset = GetOffset(splited,len,ref countedWords);
            var stack = new List<string>();
            for (int j = 0; j < offset;j++)
            {
              stack.Add(splited[countedWords++].Trim());
            }
            int stackWordsCount = stack.Sum(a => a.Length);
            int neededSpaces = len - stackWordsCount;
            int spaceForEachWord = neededSpaces / stack.Count();
            for (int k = 0 ; k < stack.Count;k++)
            {
                if(k == 0)
                  finall += stack[k];
                if (neededSpaces != 0)
                {
                  finall += GiveMeSpace(k == stack.Count - 1 ? neededSpaces : spaceForEachWord);
                  neededSpaces -= spaceForEachWord;
                }
                if(k != 0)
                  finall += stack[k];
            }
            finall += '\n';
          }
          return finall.Trim();
        }

        private static int GetNeededSpaceCount(int neededSpaces, int v)
        {
          int temp = 0;
          while (++temp * v >= neededSpaces);
          return temp;
        }

        public static List<string> SplitByWord(string str)
        {
          var finall = new List<string>();
         
          string temp = "";
          foreach (var item in str)
          {
            if (item == ' ' || item == '\n' || item == '\t' || item == '\r')
            {
              if (temp != string.Empty)
                finall.Add(temp);
              temp = "";
            }
            else
            {
              temp += item.ToString();
            }             
          }
          return finall;
        }
        
        public static int GetOffset(List<string> str,int len,ref int countedWords)
        {
          int offset = 0;
          int totoalLength = 0;
          int temp = countedWords;
          while (totoalLength <= len && temp < str.Count)
          {
            totoalLength += str[temp].Length;
            totoalLength++;
            if (offset > 1 && totoalLength > len || totoalLength == len) break; 
            offset++;
            temp++;
          }
          return offset;
        }
        
        public static string GiveMeSpace(int len)
        {
          string temp = "";
          for (int i = 0; i < len;i++)
            temp += " ";
          return temp;
        }
    }
}
