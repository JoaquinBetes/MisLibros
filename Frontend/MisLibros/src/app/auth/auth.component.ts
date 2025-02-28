import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService, RegisterDto } from './auth.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ErrorModalComponent } from '../shared/error-modal/error-modal.component';

@Component({
  selector: 'app-auth',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ErrorModalComponent,
  ],
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent {
  activeTab: 'login' | 'signin' = 'login';
  signinForm: FormGroup;
  errorMessage: string | null = null;

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.signinForm = this.fb.group({
      nombreUsuario: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      nombreCompleto: ['', Validators.required],
      rol: ['', Validators.required],
      password: ['', [
        Validators.required,
        Validators.pattern('^(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z0-9]).{8,}$')
      ]]
    });
  }

  onSignInSubmit(): void {
    if (this.signinForm.invalid) {
      this.signinForm.markAllAsTouched();
      return;
    }
    const registerData: RegisterDto = this.signinForm.value;
    this.authService.register(registerData).subscribe({
      next: (res) => {
        console.log('Usuario registrado exitosamente:', res);
        // Aquí podrías redirigir o notificar al usuario
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Ocurrió un error inesperado al registrar el usuario.';
      }
    });
  }

  setActiveTab(tab: 'login' | 'signin'): void {
    this.activeTab = tab;
    this.errorMessage = null;
  }

  closeModal(): void {
    this.errorMessage = null;
  }
}
