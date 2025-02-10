namespace Questao2;

internal class FootballMatches
{
   public int Page { get; set; }
   public int PerPage { get; set; }
   public int Total { get; set; }
   public int TotalPages { get; set; }
   public IEnumerable<Match>? Data { get; set; }
}