import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './shared/guards/auth.guard';
import { DashboardComponent } from './dashboard/dashboard.component';

const routes : Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent,  canActivate: [AuthGuard] },
  { path: 'authentication', loadChildren: () => import('./features/authentication/authentication.module').then(m => m.AuthenticationModule) },
  { path: 'vault', loadChildren: () => import('./features/vault/vault.module').then(m => m.VaultModule), canActivate:[AuthGuard] },
  { path: 'statement', loadChildren: () => import('./features/statement/statement.module').then(m => m.StatementModule), canActivate: [AuthGuard] },
  { path: 'user', loadChildren: () => import('./features/user/user.module').then(m => m.UserModule), canActivate: [AuthGuard] },
  { path: '**', redirectTo: 'dashboard' }
]

@NgModule({
  imports: [
    RouterModule.forRoot(routes ,{ preloadingStrategy: PreloadAllModules, relativeLinkResolution: 'legacy' }),
  ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
