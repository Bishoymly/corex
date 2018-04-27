using System;
using System.Collections.Generic;
using System.Text;
using CoreX.Models;

namespace CoreX.Emit
{
    public interface ICodeGenerator
    {
        void GenerateCode(Model model);
    }
}
