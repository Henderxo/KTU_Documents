#!/usr/bin/perl -w

if (@ARGV != 0) {
    die "Dosn't need arguments\n";
}


open my $failas, '<', '/home/stud/stud/access_log' or die "Nepavyko atidaryti failo: $!";

my %Ips; 


while (<$failas>) {
    chomp;
	s/\s+/ /go;

    my @fields = split /\s+/, $_;

    # Make sure there are enough fields to avoid "uninitialized value" warnings
    next unless @fields >= 7;


    ($ip, $busenosKodas, $baitai) =
      ($fields[0], $fields[8], $fields[9]);
	 
	 
	if (!defined $busenosKodas || $busenosKodas ne '' && $busenosKodas !~ /^\d+$/) {
		next; 
	}
	if (!defined $ip || $busenosKodas != 404)
	{
		next;
	}
	
    if (!exists $Ips{$ip}) {
        $Ips{$ip} = {
            count => 1,
            bytes => $baitai
        };
    } else {
        $Ips{$ip}{count}++;
        $Ips{$ip}{bytes} += $baitai;
    }



}
close $failas;

foreach my $ip (keys %Ips) {
    print "$ip - Count: $Ips{$ip}{count}, Bytes: $Ips{$ip}{bytes}\n";
}



