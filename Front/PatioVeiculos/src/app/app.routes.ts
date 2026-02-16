import { Routes } from '@angular/router';
import {PatioAgora} from './components/patio-agora/patio-agora';
import {Veiculos} from './components/veiculos/veiculos';
import {CadastrarVeiculo} from './components/veiculos/cadastrar-veiculo/cadastrar-veiculo';
import {ListaMovimentacoes} from './components/patio-agora/lista-movimentacoes/lista-movimentacoes';
import {CadastrarEntrada} from './components/patio-agora/cadastrar-entrada/cadastrar-entrada';
import {EditarVeiculo} from './components/veiculos/editar-veiculo/editar-veiculo';
import {ListaVeiculos} from './components/veiculos/lista-veiculos/lista-veiculos';
import {Relatorios} from './components/relatorios/relatorios';
import {RelatorioFaturamento} from './components/relatorios/relatorio-faturamento/relatorio-faturamento';

export const routes: Routes = [
  {path: '', redirectTo: 'patio-agora/lista', pathMatch: 'full' },
  {path: 'patio-agora', redirectTo: 'patio-agora/lista', pathMatch: 'full' },
  {path: 'patio-agora', component: PatioAgora, children: [
      {path: 'lista', component: ListaMovimentacoes },
      {path: 'cadastrar', component: CadastrarEntrada
  }]},
  {path: 'veiculos', redirectTo: 'veiculos/lista', pathMatch: 'full' },
  {path: 'veiculos', component: Veiculos, children: [
      {path: 'lista', component: ListaVeiculos },
      {path: 'editar/:id', component: EditarVeiculo },
      {path: 'cadastrar', component: CadastrarVeiculo }
  ]},
  {path: 'relatorios', redirectTo: 'relatorios/faturamento', pathMatch: 'full' },
  {path: 'relatorios', component: Relatorios, children: [
      {path: 'faturamento', component: RelatorioFaturamento },
  ]}
];
