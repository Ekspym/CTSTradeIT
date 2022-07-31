using CTSTradeIT;

HtmlCareerReader htmlCareerReader = new HtmlCareerReader();

List<string> Careers = htmlCareerReader.getAllJobOffers(true);

Console.WriteLine("Zadejte číslo pracovní nabídky pro stažení obsahu „Co vás na této pozici čeká“.");
string userInput = Console.ReadLine();
int numericInput;
bool isNumeric = int.TryParse(userInput, out numericInput);
while (!isNumeric || numericInput <=0 || numericInput > Careers.Count)
{
    Console.WriteLine("Nezadali jste číslo nebo neodpovídalo označeným pracovním nabídkám.");
    Console.WriteLine("Zadejte číslo pracovní nabídky pro stažení obsahu „Co vás na této pozici čeká“.");
     userInput = Console.ReadLine();
     isNumeric = int.TryParse(userInput, out numericInput);
}
htmlCareerReader.downloadWhatToExpectSection(numericInput);
while(userInput != "")
{ 
Console.WriteLine("Pokud chcete stáhnout obsah další pracovní nabídky zadejte odpovídající číslo.");
 userInput = Console.ReadLine();
    
 isNumeric = int.TryParse(userInput, out numericInput);
    if(isNumeric && numericInput > 0 && numericInput <= Careers.Count)
 htmlCareerReader.downloadWhatToExpectSection(numericInput);
    else
        Console.WriteLine("Nezadali jste číslo nebo neodpovídalo označeným pracovním nabídkám.\nPokud chcete aplikaci ukončit zmáčkněte enter");
}
Console.Read();
