import { Routes } from '@angular/router';
import { Home } from '../features/home/home';
import { MemberList } from '../features/members/member-list/member-list';
import { MemberDetails } from '../features/members/member-details/member-details';
import { Messages } from '../features/messages/messages';
import { Lists } from '../features/lists/lists';

export const routes: Routes = [
    {path: '', component: Home},
    {path: 'members', component: MemberList},
    {path: 'members/:id', component: MemberDetails},
    {path: 'lists', component: Lists},
    {path: 'messages', component: Messages},
    {path: '**', component: Home},

];
