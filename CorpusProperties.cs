
namespace CompareFrequencies
{
    internal class CorpusProperties
    {

        public string FileLocation { get; }
        public string FileEncoding { get; }
        public string Quote { get; }
        public string Delimiter { get; }
        public string Name { get; }
        public ulong Size { get; }


        public CorpusProperties(string fileLoc, string fileEnc, string quotechar, string delimitchar, string corpname, ulong corpsize)
        {
            FileLocation = fileLoc;
            FileEncoding = fileEnc;
            Quote = quotechar;
            Delimiter = delimitchar;
            Name = corpname;
            Size = corpsize;
        }


    }
}
