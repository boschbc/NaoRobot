# Include local configuration.
include Makefile.local

BINDIR=build/bin
OBJDIR=build/tmp
SOURCES=nao.cpp
LIBS=alerror alproxies

OBJS=$(patsubst %.cpp,$(OBJDIR)/%.o,$(SOURCES))
CXXFLAGS=-I$(SDKDIR)/include
LDFLAGS=-L$(SDKDIR)/lib $(foreach lib,$(LIBS),-l$(lib))

all: $(OBJS)
	@echo Linking main binary...
	@$(CXX) $(LDFLAGS) -o $(BINDIR)/$(BIN) $(OBJS)
	@echo Copying over required libraries...
	@cp $(SDKDIR)/lib/lib* $(LIBDIR)

clean:
	@rm -rf $(LIBDIR)/*
	@rm -f $(OBJDIR)/*
	@rm -f $(BINDIR)/$(BIN)

$(OBJDIR)/%.o: src/%.cpp
	@echo Compiling $< to $@...
	@$(CXX) $(CXXFLAGS) -c $< -o $@
