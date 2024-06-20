#!/usr/bin/perl -w

if (@ARGV != 1) {
    die "Usage: $0 <target_ip>\n";
}

my $target_ip = $ARGV[0];
open my $failas, '<', '/home/stud/stud/access_log' or die "Nepavyko atidaryti failo: $!";

my $ip_count = 0;
my $total_bytes = 0;

while (<$failas>) {
    chomp;
   my @fields = split /\s+/, $_;

    next unless @fields >= 7;

    ($ip, $busenosKodas, $baitai) =
      ($fields[0], $fields[8], $fields[9]);

    if ($ip eq $target_ip) {
        $ip_count++; 
		if($baitai ne "-")
		{
			$total_bytes += $baitai; 
		}
		
        
    }
}

close $failas;

print "$total_bytes \n";

open my $rez_failas, '>', 'rez' or die "Nepavyko atidaryti rez failo: $!";
print $rez_failas "IP adresu $target_ip buvo rasta $ip_count kartus ir perduota $total_bytes baitų.\n";
close $rez_failas;