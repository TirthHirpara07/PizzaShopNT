namespace Entity.Helper;

public class ItemPagination
{
        public int page { get; set; }
        public int pageSize { get; set; }
        public int maxPage { get; set; }
        public int count { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public string search { get; set; }

        //also used for ModifierGroup id
        public int Category  { get; set; }
        public int ModifierGroup  { get; set; }
        public string Currentsection { get; set;}


        public ItemPagination()
        {
            Currentsection = "item";
            pageSize = 3;
            search = "";
        }
}
