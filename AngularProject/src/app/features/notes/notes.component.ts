import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Note } from '../../models/note.model';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrl: './notes.component.css'
})
export class NotesComponent {
  @Input() note: Note | null = null;
  @Output() save = new EventEmitter<Note>();
  
  private audioChunks: any[] = [];
  public isRecording: boolean = false;
  public audioData: Blob | null = null;
  public notesForm: FormGroup;
  private mediaRecorder!: MediaRecorder;

  constructor(private fb: FormBuilder) {
    this.notesForm = this.fb.group({
      id: [0],
      title: ['', Validators.required],
      description: ['', Validators.required],
      recordFile: [null]
    });
  }

  ngOnChanges() {
    if (this.note) {
      this.notesForm.patchValue(this.note);
    } else {
      this.notesForm.reset({ id: 0, title: '', description: '' });
    }
  }

  startRecording() {
    this.audioChunks = [];
    navigator.mediaDevices.getUserMedia({ audio: true }).then((stream) => {
      this.mediaRecorder = new MediaRecorder(stream);

      this.mediaRecorder.ondataavailable = (event: any) => {
        this.audioChunks.push(event.data);
      };

      this.mediaRecorder.onstop = () => {
        this.audioData = new Blob(this.audioChunks, { type: 'audio/wav' });
      };

      this.mediaRecorder.start();
      this.isRecording = true;
    }).catch(err => {
      alert(`Error accessing microphone, ${err}`);
    });
  }

  stopRecording() {
    this.mediaRecorder.stop();
    this.isRecording = false;
  }

  clearRecord() {
    this.audioChunks = [];
    this.audioData = null;
    this.isRecording = false;
  }

  onSubmit() {
    if (this.notesForm.invalid) {
      this.notesForm.markAllAsTouched();
      return;
    }

    if (this.audioData != null) {
      this.notesForm.patchValue({
        recordFile: this.audioData
      });
    }

    this.save.emit(this.notesForm.value);
    this.notesForm.reset();

  }
}
