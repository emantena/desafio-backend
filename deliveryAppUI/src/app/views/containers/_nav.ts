export interface INavData {
  name?: string;
  url?: string | any[];
  role: string;
}

export const navItems: INavData[] = [
  {
    name: 'dashboard',
    url: '/dashboard',
    role: 'adminstrator',
  },
  {
    name: 'veículos',
    url: '/vehicle',
    role: 'adminstrator',
  },
  {
    name: 'pedidos',
    url: '/order',
    role: 'adminstrator',
  },
];
