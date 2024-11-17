import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Note } from '../models/note.model';

@Injectable({
  providedIn: 'root',
})
export class NotesService {
  private apiUrl = 'https://localhost:7011/api/notes';

  constructor(private http: HttpClient) {}

  getNotes(): Observable<Note[]> {
    return this.http.get<Note[]>(this.apiUrl);
  }

  addNote(note: Note): Observable<Note> {
    const formData = new FormData();
      
    formData.append('title', note.title);
    formData.append('description', note.description);
    formData.append('recordFile', note.recordFile);

    return this.http.post<Note>(this.apiUrl, formData);
  }

  updateNote(note: Note): Observable<Note> {
    const formData = new FormData();
      
    formData.append('title', note.title);
    formData.append('description', note.description);
    formData.append('recordFile', note.recordFile);

    return this.http.put<Note>(`${this.apiUrl}/${note.id}`, formData);
  }

  deleteNote(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  downloadAudio(fileName: string): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/${fileName}`, {responseType:'blob'});
  }
}