using System.ComponentModel.DataAnnotations;

namespace VotingData.Models
{
    public class Counts
    {
        public int ID { get; set; }
        public string Candidate { get; set; }
        public int Count { get; set; }
    }
}
