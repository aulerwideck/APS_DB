﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APS_DB.Classes
{
	public class searchElement
	{
		private string nomeColuna;
		private Control control;

		public Control Controle { get => control; set => control = value; }
		public string NomeColuna { get => nomeColuna; set => nomeColuna = value; }

		public searchElement(string nomeCol)
		{
			NomeColuna = nomeCol;
		}
	}
}