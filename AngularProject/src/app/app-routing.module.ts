import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistrationComponent } from './features/registration/registration.component';
import { LoginComponent } from './features/login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { NotesListComponent } from './features/notes-list/notes-list.component';


const routes: Routes = [
  { path: 'registration', component: RegistrationComponent },     
  { path: 'login', component: LoginComponent },
  { path: 'notes', component: NotesListComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
];



@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
