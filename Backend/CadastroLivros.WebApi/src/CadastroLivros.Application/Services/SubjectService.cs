using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using CadastroLivros.Domain.Entities;
using CadastroLivros.Domain.Execeptions;
using CadastroLivros.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CadastroLivros.Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddSubjectAsync(SubjectEntity subject)
        {
            try
            {
                _unitOfWork.Subject.Add(subject);
                var result = await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (DbException ex)
            {
                throw new ApplicationException("Erro ao adicionar o assunto no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao adicionar o assunto.", ex);
            }
        }

        public async Task<bool> UpdateSubjectAsync(SubjectEntity subject)
        {
            try
            {
                var existingSubject = await _unitOfWork.Subject.GetByIdAsync(x => x.SubjectId == subject.SubjectId);
                if (existingSubject == null)
                {
                    throw new NotFoundException($"Assunto  com ID {subject.SubjectId} não encontrado.");
                }

                existingSubject.Description = subject.Description;
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ApplicationException("Conflito de concorrência ao atualizar o assunto.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Erro ao atualizar o assunto no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao atualizar o assunto.", ex);
            }
        }

        public async Task<IEnumerable<SubjectEntity>> GetAllSubjectsAsync()
        {
            try
            {
                return await _unitOfWork.Subject.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao buscar todos os assuntos.", ex);
            }
        }

        public async Task<SubjectEntity> GetSubjectByIdAsync(int subjectId)
        {
            try
            {
                return await _unitOfWork.Subject.GetByIdAsync(x => x.SubjectId == subjectId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao buscar o assunto com ID {subjectId}.", ex);
            }
        }

        public async Task<bool> DeleteSubjectAsync(SubjectEntity subject)
        {
            try
            {
                var existingSubject = await _unitOfWork.Subject.GetByIdAsync(x => x.SubjectId == subject.SubjectId);
                if (existingSubject == null)
                {
                    return false;
                }

                _unitOfWork.Subject.Remove(existingSubject);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("Erro ao deletar o assunto no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao deletar o assunto.", ex);
            }
        }
    }
}
