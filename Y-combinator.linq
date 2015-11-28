<Query Kind="Program" />

delegate int F(int x);
delegate F G(F f);

F Y(G g)
{
	return x => g(Y(g))(x);
}

void Main()
{
	// f is a fixed point of function inside Y(...)
	Y(f => x =>	x == 0 ? 1 : x * f(x - 1))(5).Dump();
}