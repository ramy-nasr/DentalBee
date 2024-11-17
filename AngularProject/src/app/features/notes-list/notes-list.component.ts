import { Component } from '@angular/core';
import { Note } from '../../models/note.model';
import { NotesService } from '../../services/notes.service';

@Component({
  selector: 'app-notes-list',
  templateUrl: './notes-list.component.html',
  styleUrl: './notes-list.component.css'
})
export class NotesListComponent {
  notes: Note[] = [];
  editingNote: Note | null = null;
  audio = new Audio();
  audioplaying = false;

  constructor(private notesService: NotesService) { }

  ngOnInit(): void {
    this.fetchNotes();
  }

  fetchNotes(): void {
    this.notesService.getNotes().subscribe((data) => {
      this.notes = data;
    });
  }

  handleSave(note: Note): void {
    if (note.id) {
      this.notesService.updateNote(note).subscribe(() => {
        const index = this.notes.findIndex(item => item.id === note.id);
        this.notes[index] = note;
      });
    } else {
      this.notesService.addNote(note).subscribe(() => {
        this.notes.push(note);
      });
    }
  }

  handleEdit(note: Note): void {
    this.editingNote = { ...note };
  }

  playAudio(filename: string) {
    this.notesService.downloadAudio(filename).subscribe(audioBlob => {
      this.audio.src = URL.createObjectURL(audioBlob);
      this.audio.load();
      this.audio.play();
      this.audioplaying = true;
    }, err => {

    });
  }

  handleDelete(noteId: string): void {
    const isConfirmed = window.confirm('Are you sure you want to delete this item?');
    if (!isConfirmed) {
      return;
    }

    this.notesService.deleteNote(noteId).subscribe(() => {
      const index = this.notes.findIndex(item => item.id === noteId);
      this.notes.splice(index, 1);
    }, error => {

    });
  }
}
