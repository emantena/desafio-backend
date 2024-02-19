export interface INavData {
  name?: string;
  url?: string | any[];
  role: string;
}

export const navDeliveryMan: INavData[] = [
  {
    name: 'planos',
    url: '/plan/rent-vehicle',
    role: 'deliveryman',
  },
  {
    name: 'minha conta',
    url: '/account',
    role: 'deliveryman',
  },
  {
    name: 'meus pedidos',
    url: '/order/my-orders',
    role: 'deliveryman',
  },
];
