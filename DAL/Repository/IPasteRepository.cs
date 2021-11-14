using System;
using System.Collections.Generic;
using DAL.Models;

namespace DAL.Repository
{
    public interface IPasteRepository : IDisposable
    {
        IEnumerable<Paste> GetPastes();
        IEnumerable<Paste> GetPastesFromLanguage(string language);
        Paste GetPasteByID(int pasteId);
        bool InsertPaste(Paste paste);
        bool DeletePaste(int pasteId);
        bool UpdatePaste(Paste paste);
        void Save();
    }
}
