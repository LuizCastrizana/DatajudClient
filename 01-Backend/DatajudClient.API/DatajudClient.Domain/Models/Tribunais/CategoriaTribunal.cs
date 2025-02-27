﻿namespace DatajudClient.Domain.Models.Tribunais
{
    public class CategoriaTribunal : ModelBase
    {
        public string Descricao { get; set; }
        public virtual List<Tribunal>? Tribunais { get; set; }

        public CategoriaTribunal()
        {
            Descricao = string.Empty;
        }
    }
}
