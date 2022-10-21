
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
namespace Lab2Solution;
/**
 * I had to put my unit test inside my lab2 solution, no matter what I did, I couldn't to get "using Lab2Solution" to work,
 * meaning I couldn't instantiate anything from lab 2. Additionally, my lab 3 seems to be glitching out, so I used lab 2
 */

public class Tests
{
    IBusinessLogic logic;
    [SetUp]
    public void Setup()
    {
        logic = new BusinessLogic();
    }

    [Test]
    public void AddTests()
    {
        Assert.That(logic.AddEntry("Clue", "Answer",0,"11/11/1111"), Is.EqualTo(InvalidFieldError.NoError));
        Assert.That(logic.AddEntry("000000000000000000000000000000000000000000000000000000000000000000000000" +
            "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000000000000000000000000000000000000", "Answer", 0, "11/11/1111"), 
            Is.EqualTo(InvalidFieldError.InvalidClueLength));

        Assert.That(logic.AddEntry("Clue", "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
            0, "11/11/1111"), Is.EqualTo(InvalidFieldError.InvalidAnswerLength));

        Assert.That(logic.AddEntry("Clue", "Answer", 0, "11/11/111"), Is.EqualTo(InvalidFieldError.InvalidDate));
        Assert.That(logic.AddEntry("Clue", "Answer", 0, "11/11/11111"), Is.EqualTo(InvalidFieldError.InvalidDate));
        Assert.That(logic.AddEntry("Clue", "Answer", 0, "1111111111"), Is.EqualTo(InvalidFieldError.InvalidDate));

        Assert.That(logic.AddEntry("Clue", "Answer", -1, "11/11/1111"), Is.EqualTo(InvalidFieldError.InvalidDifficulty));
        Assert.That(logic.AddEntry("Clue", "Answer", 4, "11/11/1111"), Is.EqualTo(InvalidFieldError.InvalidDifficulty));
    }

    [Test]
    public void EditTests()
    {
        Assert.That(logic.EditEntry("Clue", "Answer", 0, "11/11/1111", 1), Is.EqualTo(EntryEditError.NoError));
        Assert.That(logic.EditEntry("000000000000000000000000000000000000000000000000000000000000000000000000" +
            "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000000000000000000000000000000000000", "Answer", 0, "11/11/1111", 1),
            Is.EqualTo(EntryEditError.InvalidFieldError));

        Assert.That(logic.EditEntry("Clue", "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
            0, "11/11/1111",1), Is.EqualTo(EntryEditError.InvalidFieldError));

        Assert.That(logic.EditEntry("Clue", "Answer", 0, "11/11/111",1), Is.EqualTo(EntryEditError.InvalidFieldError));
        Assert.That(logic.EditEntry("Clue", "Answer", 0, "11/11/11111",1), Is.EqualTo(EntryEditError.InvalidFieldError));
        Assert.That(logic.EditEntry("Clue", "Answer", 0, "1111111111",1), Is.EqualTo(EntryEditError.InvalidFieldError));

        Assert.That(logic.EditEntry("Clue", "Answer", -1, "11/11/1111",1), Is.EqualTo(EntryEditError.InvalidFieldError));
        Assert.That(logic.EditEntry("Clue", "Answer", 4, "11/11/1111",1), Is.EqualTo(EntryEditError.InvalidFieldError));

        Assert.That(logic.EditEntry("Clue", "Answer", 0, "11/11/1111", -1), Is.EqualTo(EntryEditError.EntryNotFound));
    }

    [Test]
    public void DeleteTests()
    {
        logic.AddEntry("Clue", "Difficulty", 0, "11/11/1111");
        System.Collections.ObjectModel.ObservableCollection<Entry> list = logic.GetEntries();

        Assert.That(logic.DeleteEntry(list.Last().Id), Is.EqualTo(EntryDeletionError.NoError));
    }

    [Test]
    public void FindEntryTests()
    {
        Assert.That(logic.FindEntry(logic.GetEntries().Last().Id), Is.Not.Null);
    }

    [Test]
    public void GetEntriesTests()
    {
        Assert.That(logic.GetEntries, Is.Not.Null);
    }

}