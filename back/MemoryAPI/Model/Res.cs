using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public record Res(bool IsSuccess, string ResMsg);
    public record Num(int num);
    public record Numplus(int num,string lexicon);
}
