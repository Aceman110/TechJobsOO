namespace TechJobs.Models
{
    public class JobField
    {
        public int ID { get; set; }
        private static int nextId = 1;

        public string Value { get; set; }

        public JobField()
        {
            ID = nextId;
            nextId++;
        }

        public JobField(string value) : this()
        {
            Value = value;
        }

       
        public bool Contains(string testValue)
        {
            return Value.ToLower().Contains(testValue.ToLower());
        }

        public override string ToString()
        {
            return Value;
        }

        
        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            return (obj as JobField).ID == ID;
        }

        
        public override int GetHashCode()
        {
            return ID;
        }

    }
}
