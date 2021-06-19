using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repository
{
    public class PasteRepository : IPasteRepository
    {
        private readonly LivePasteContext _context;
        private bool _disposed;

        public PasteRepository(LivePasteContext context)
        {
            _context = context;
            _disposed = false;
        }

        public bool DeletePaste(int pasteId)
        {
            Paste paste = _context.Pastes.Find(pasteId);
            if (paste == null)
            {
                return false;
            }
            _context.Pastes.Remove(paste);
            return true;
        }

        public Paste GetPasteByID(int pasteId)
        {
            return _context.Pastes.Find(pasteId);
        }

        public IEnumerable<Paste> GetPastes()
        {
            return _context.Pastes.ToList();
        }

        public IEnumerable<Paste> GetPastes(string language)
        {
            if (language == null)
                return _context.Pastes.ToList();
            return _context.Pastes.Where(paste => paste.Language.ToLower() == language.ToLower());
        }

        public bool InsertPaste(Paste paste)
        {
            _context.Pastes.Add(paste);
            return true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool UpdatePaste(Paste paste)
        {
            Paste existingPaste = _context.Pastes.Find(paste.PasteID);
            if (existingPaste != null)
            {
                existingPaste.Content = paste.Content;
                //existingPaste.CreationDate = paste.CreationDate;
                
                return true;
            }
            return false; 
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés)
                }

                _context.Dispose();
                _disposed = true;
            }
        }

        ~PasteRepository()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
