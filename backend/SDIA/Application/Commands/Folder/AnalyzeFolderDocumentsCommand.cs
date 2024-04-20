using Application.DTOs;
using Domain.Constants;
using MediatR;

namespace Application.Commands.Folder;

public record AnalyzeFolderDocumentsCommand(Guid FolderId,
    FolderType FolderType,
    List<DocumentType> Documents) : IRequest<AnalyzeFolderDto>;